using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay.Singleton
{
    public class SingletonAdmin
    {
        private static SingletonAdmin instance = null;

        public string ConnectionString { get; private set; }
        public Admin AdminConnecte { get; private set; }

        private SingletonAdmin()
        {
            ConnectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq2;Uid=2146340;Pwd=2146340;";
            AdminConnecte = null;
        }

        public static SingletonAdmin getInstance()
        {
            if (instance == null)
                instance = new SingletonAdmin();
            return instance;
        }

   
        public bool CreerAdmin(string nomUtilisateur, string motDePasse)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomUtilisateur) || string.IsNullOrWhiteSpace(motDePasse))
                    return false;

                using MySqlConnection con = new(ConnectionString);
                con.Open();

           
                using MySqlCommand checkCmd = new("SELECT COUNT(*) FROM utilisateur WHERE nomUtilisateur=@u", con);
                checkCmd.Parameters.AddWithValue("@u", nomUtilisateur);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (count > 0)
                    return false;

                string hashPass = HashMotDePasse(motDePasse);

                using MySqlCommand cmd = new(
                    "INSERT INTO utilisateur (nomUtilisateur, motDePasse, estAdmin) VALUES (@u, @p, 1)", con);
                cmd.Parameters.AddWithValue("@u", nomUtilisateur);
                cmd.Parameters.AddWithValue("@p", hashPass);

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur MySQL lors de la création de l'admin : " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur inattendue lors de la création de l'admin : " + ex.Message);
                return false;
            }
        }

        // Hashage du mot de passe avec SHA-256 avec les note de cours
        private string HashMotDePasse(string motDePasse)
        {
            try
            {
                using SHA256 sha = SHA256.Create();
                byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(motDePasse));
                return Convert.ToHexString(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du hash du mot de passe : " + ex.Message);
                return null;
            }
        }

        public bool Connecter(string nomUtilisateur, string motDePasse)
        {
            try
            {
                string hashPass = HashMotDePasse(motDePasse);

                if (hashPass == null)
                    return false;

                using MySqlConnection con = new(ConnectionString);
                con.Open();

                using MySqlCommand cmd = new(
                    "SELECT * FROM utilisateur WHERE nomUtilisateur=@u AND motDePasse=@p AND estAdmin=1", con);

                cmd.Parameters.AddWithValue("@u", nomUtilisateur);
                cmd.Parameters.AddWithValue("@p", hashPass);

                using var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    AdminConnecte = new Admin(nomUtilisateur, motDePasse, true);
                    return true;
                }

                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur MySQL lors de la connexion : " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur inattendue lors de la connexion : " + ex.Message);
                return false;
            }
        }


        public void Deconnecter()
        {
            try
            {
                AdminConnecte = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la déconnexion : " + ex.Message);
            }
        }

        public bool EstAdminConnecte()
        {
            try
            {
                return AdminConnecte != null && AdminConnecte.EstAdmin;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la vérification de l'état admin : " + ex.Message);
                return false;
            }
        }
    }
}