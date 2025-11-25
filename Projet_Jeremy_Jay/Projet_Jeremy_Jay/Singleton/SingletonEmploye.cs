using MySql.Data.MySqlClient;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay
{
    class SingletonEmploye
    {

        string connectionString;
        ObservableCollection<Classes.Employe> ListeEmploye;

        static SingletonEmploye instance = null;

        private SingletonEmploye()
        {
          
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq2;Uid=2146340;Pwd=2146340;";

          
            ListeEmploye = new ObservableCollection<Classes.Employe>();
        }
        public static SingletonEmploye getInstance()
        {

            if (instance == null)
                instance = new SingletonEmploye();

          
            return instance;
        }



        public ObservableCollection<Classes.Employe> Liste
        {
            get => ListeEmploye;  
        }

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
                    DateTime date_naissance =r.GetDateTime("date_naissance");
                    DateTime date_embauche = r.GetDateTime("date_embauche");
                    string matricule = r.GetString("matricule");
                    string num_projet = r.GetString("num_projet");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    string email = r.GetString("email");
                    string adresse = r.GetString("adresse");
                    string photo_id = r.GetString("photo_id");
                    string statut = r.GetString("statut");

                    
                    Employe e = new Employe(matricule, num_projet , prenom, nom,date_naissance,email,adresse,date_embauche,taux_horaire,photo_id,statut);

                  
                    ListeEmploye.Add(e);
                }
            }
            catch (MySqlException ex)
            {
           
                Debug.WriteLine(ex.Message);
            }
        }


        public void ajouterEmploye(string matricule, string num_projet, string prenom, string nom, DateTime date_naissance, string email, string adresse, DateTime date_embauche, double taux_horaire, string photo_id, string statut)
        {
            using MySqlConnection con = new MySqlConnection(connectionString);
            using MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = @"INSERT INTO employe (matricule, num_projet, prenom, nom, date_naissance, email, adresse, date_embauche, taux_horaire, photo_id, statut)
                VALUES (NULL, @num_projet, @prenom, @nom, @date_naissance, @email,@adresse, @date_embauche, @taux_horaire, @photo_id, @statut)";

          
            cmd.Parameters.AddWithValue("@num_projet", num_projet);
            cmd.Parameters.AddWithValue("@prenom", prenom);
            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@date_naissance", date_naissance);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@adresse", adresse);
            cmd.Parameters.AddWithValue("@date_embauche", date_embauche);
            cmd.Parameters.AddWithValue("@taux_horaire", taux_horaire);
            cmd.Parameters.AddWithValue("@photo_id", photo_id);
            cmd.Parameters.AddWithValue("@status", statut);

            con.Open();
            cmd.ExecuteNonQuery();

           
            getAllEmploye();
        }

        public void SupprimerEmploye(string matricule)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE FROM employe WHERE matricule = @matricule";
                cmd.Parameters.AddWithValue("@matricule", matricule);

                con.Open();
                cmd.ExecuteNonQuery();

                getAllEmploye(); 
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Erreur suppression employé : " + ex.Message);
            }
        }

        public void ModifierEmploye(string matricule, string num_projet, string prenom, string nom,
                            DateTime date_naissance, string email, string adresse,
                            DateTime date_embauche, double taux_horaire,
                            string photo_id, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = @"
            UPDATE employe
            SET num_projet = @num_projet,
                prenom = @prenom,
                nom = @nom,
                date_naissance = @date_naissance,
                email = @email,
                adresse = @adresse,
                date_embauche = @date_embauche,
                taux_horaire = @taux_horaire,
                photo_id = @photo_id,
                status = @statut
            WHERE matricule = @matricule";

                cmd.Parameters.AddWithValue("@matricule", matricule);
                cmd.Parameters.AddWithValue("@num_projet", num_projet);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@date_naissance", date_naissance);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@date_embauche", date_embauche);
                cmd.Parameters.AddWithValue("@taux_horaire", taux_horaire);
                cmd.Parameters.AddWithValue("@photo_id", photo_id);
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
