using MySql.Data.MySqlClient;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay
{
    class SingletonEmploye
    {
        string connectionString;
        ObservableCollection<Employe> ListeEmploye;

        static SingletonEmploye instance = null;

        private SingletonEmploye()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq2;Uid=2146340;Pwd=2146340;";
            ListeEmploye = new ObservableCollection<Employe>();
        }

        public static SingletonEmploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmploye();
            return instance;
        }

        public ObservableCollection<Employe> Liste => ListeEmploye;

        public void getAllEmploye()
        {
            ListeEmploye.Clear();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM employe";

                con.Open();

                using MySqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    double taux_horaire = r.GetDouble("taux_horaire");
                    DateOnly date_naissance = DateOnly.FromDateTime(r.GetDateTime("date_naissance"));
                    DateOnly date_embauche = DateOnly.FromDateTime(r.GetDateTime("date_embauche"));
                  
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    string email = r.GetString("email");
                    string adresse = r.GetString("adresse");
                    string photo = r.GetString("photo");    
                    string statut = r.GetString("statut");

                    Employe e = new Employe(matricule, prenom, nom, date_naissance,
                                            email, adresse, date_embauche,
                                            taux_horaire, photo, statut); 
                    ListeEmploye.Add(e);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ajouterEmploye(string prenom, string nom,
                              DateOnly date_naissance, string email, string adresse,
                              DateOnly date_embauche, double taux_horaire,
                            string photo, string statut)
        {
            using MySqlConnection con = new MySqlConnection(connectionString);
            using MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;

          
            cmd.CommandText = @"
        INSERT INTO employe (prenom, nom, date_naissance,
                             email, adresse, date_embauche,
                             taux_horaire, photo, statut)
        VALUES (@prenom, @nom, @date_naissance,
                @email, @adresse, @date_embauche,
                @taux_horaire, @photo, @statut)";

            cmd.Parameters.AddWithValue("@prenom", prenom);
            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@date_naissance", date_naissance);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@adresse", adresse);
            cmd.Parameters.AddWithValue("@date_embauche", date_embauche);
            cmd.Parameters.AddWithValue("@taux_horaire", taux_horaire);
            cmd.Parameters.AddWithValue("@photo", photo);
            cmd.Parameters.AddWithValue("@statut", statut);

            con.Open();
            cmd.ExecuteNonQuery();

           
            cmd.CommandText = @"
        SELECT matricule 
        FROM employe 
        WHERE nom = @nom AND prenom = @prenom AND date_naissance = @date_naissance
        ORDER BY matricule DESC
        LIMIT 1";

            string matriculeGenere = (string)cmd.ExecuteScalar();

           
            Employe e = new Employe(matriculeGenere, prenom, nom, date_naissance,
                                    email, adresse, date_embauche,
                                    taux_horaire, photo, statut);

      
            ListeEmploye.Add(e);
        }
        

        public void ModifierEmploye(string matricule, string prenom, string nom,
                                      DateOnly date_naissance, string email, string adresse,
                                     DateOnly date_embauche, double taux_horaire,
                                    string photo, string statut)
        {
            try
            {


                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
               
        cmd.CommandText = @"
                    UPDATE employe
                    SET prenom = @prenom,
                        nom = @nom,
                        date_naissance = @date_naissance,
                        email = @email,
                        adresse = @adresse,
                        date_embauche = @date_embauche,
                        taux_horaire = @taux_horaire,
                        photo = @photo,
                        statut = @statut
                    WHERE matricule = @matricule";

                cmd.Parameters.AddWithValue("@matricule", matricule);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@date_naissance", date_naissance);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@date_embauche", date_embauche);
                cmd.Parameters.AddWithValue("@taux_horaire", taux_horaire);
                cmd.Parameters.AddWithValue("@photo", photo);     
                cmd.Parameters.AddWithValue("@statut", statut);

                con.Open();
                cmd.ExecuteNonQuery();

                getAllEmploye();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Erreur modification employé : " + ex.Message);
            }
        }
    }
}

