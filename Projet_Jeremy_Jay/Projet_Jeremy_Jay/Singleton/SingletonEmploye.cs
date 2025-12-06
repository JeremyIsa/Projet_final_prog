using MySql.Data.MySqlClient;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        public ObservableCollection<Employe> ListeEmploye;

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
                using MySqlCommand cmd = new MySqlCommand("liste_employes", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
                Debug.WriteLine("Erreur liste_emplyes : " + ex.Message);
            }
        }


        public void ajouterEmploye(string prenom, string nom,
                                   DateOnly date_naissance, string email, string adresse,
                                   DateOnly date_embauche, double taux_horaire,
                                   string photo, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("ajout_employe", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@i_prenom", prenom);
                cmd.Parameters.AddWithValue("@i_nom", nom);
                cmd.Parameters.AddWithValue("@i_date_naissance", date_naissance);
                cmd.Parameters.AddWithValue("@i_email", email);
                cmd.Parameters.AddWithValue("@i_adresse", adresse);
                cmd.Parameters.AddWithValue("@i_date_embauche", date_embauche);
                cmd.Parameters.AddWithValue("@i_taux_horaire", taux_horaire);
                cmd.Parameters.AddWithValue("@i_photo", photo);
                cmd.Parameters.AddWithValue("@i_statut", statut);

                con.Open();
                cmd.ExecuteNonQuery();


                getAllEmploye();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Erreur ajouter_employe : " + ex.Message);
            }
        }



        public void ModifierEmploye(
     string matricule,
     string prenom,
     string nom,
     DateOnly date_naissance,
     string email,
     string adresse,
     DateOnly date_embauche,
     double taux_horaire,
     string photo,
     string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("modifier_employe", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", matricule);
                cmd.Parameters.AddWithValue("@i_nom", nom);
                cmd.Parameters.AddWithValue("@i_prenom", prenom);
                cmd.Parameters.AddWithValue("@i_email", email);
                cmd.Parameters.AddWithValue("@i_adresse", adresse);
                cmd.Parameters.AddWithValue("@i_taux_horaire", taux_horaire);
                cmd.Parameters.AddWithValue("@i_photo", photo);

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


