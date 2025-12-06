using MySql.Data.MySqlClient;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay
{
    class SingletonProjet
    {

        string connectionString;
        public ObservableCollection<Projet> ListeProjet;

        static SingletonProjet instance = null;

        private SingletonProjet()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq2;Uid=6235801;Pwd=6235801;";
            ListeProjet = new ObservableCollection<Projet>();
        }

        public static SingletonProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonProjet();
            return instance;
        }

        public ObservableCollection<Projet> Liste => ListeProjet;

        public void getAllProjet()
        {
            ListeProjet.Clear();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("liste_projets", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                using MySqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string num_projet = r.GetString("numero_projet");
                    string titre = r.GetString("titre");
                    DateTime date_debut = r.GetDateTime("date_debut");
                    string description = r.GetString("description");
                    double budget = r.GetDouble("budget");
                    int nb_employe = r.GetInt32("nb_employes_requis");
                    double total_salaire = r.GetDouble("total_salaires");
                    int id_client = r.GetInt32("id_client");
                    string statut = r.GetString("statut");

                    Projet p = new Projet(num_projet, titre, date_debut, description, budget, nb_employe, total_salaire, id_client, statut);

                    ListeProjet.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ajouterEmployeAuProjet(string matriculeEmploye, string numProjet, double heuresTravaillees, double salaire)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("ajout_employe_projet", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@i_matricule", matriculeEmploye);
                cmd.Parameters.AddWithValue("@i_numero_projet", numProjet);
                cmd.Parameters.AddWithValue("@i_heures_travailles", heuresTravaillees);
                cmd.Parameters.AddWithValue("@i_salaire", salaire);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'assignation : {ex.Message}");
            }
        }

        public List<Projet> obtenirProjetsPourEmploye(string matricule)
        {
            List<Projet> projets = new List<Projet>();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("SELECT p.* FROM Projet p INNER JOIN Employe_Projet ep ON p.numero_projet = ep.numero_projet WHERE ep.matricule = @matricule", con);

                cmd.Parameters.AddWithValue("@matricule", matricule);

                con.Open();

                using MySqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    string num_projet = r.GetString("numero_projet");
                    string titre = r.GetString("titre");
                    DateTime date_debut = r.GetDateTime("date_debut");
                    string description = r.GetString("description");
                    double budget = r.GetDouble("budget");
                    int nb_employe = r.GetInt32("nb_employes_requis");
                    double total_salaire = r.GetDouble("total_salaires");
                    int id_client = r.GetInt32("id_client");
                    string statut = r.GetString("statut");

                    Projet p = new Projet(num_projet, titre, date_debut, description, budget, nb_employe, total_salaire, id_client, statut);
                    projets.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des projets : {ex.Message}");
            }

            return projets;
        }
    }
}
