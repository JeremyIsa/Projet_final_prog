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


        public bool AjouterProjet(Projet projet)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    conn.Open();

                
                    string sql = @"
                        INSERT INTO projet (id_client, date_debut, titre, description, budget, nb_employes_requis, total_salaires, statut)
                        VALUES (@id_client, @date_debut, @titre, @description, @budget, @nb_employe, @total_salaire, @statut);
                    ";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id_client", projet.Id_client);
                    cmd.Parameters.AddWithValue("@date_debut", projet.Date_debut);
                    cmd.Parameters.AddWithValue("@titre", projet.Titre);
                    cmd.Parameters.AddWithValue("@description", projet.Description);
                    cmd.Parameters.AddWithValue("@budget", projet.Budget);
                    cmd.Parameters.AddWithValue("@nb_employe", projet.Nb_employe);
                    cmd.Parameters.AddWithValue("@total_salaire", projet.Total_salaire);
                    cmd.Parameters.AddWithValue("@statut", projet.Statut);

                    cmd.ExecuteNonQuery();

                    string num = RecupererNumeroProjet((int)cmd.LastInsertedId);

                    projet.Num_projet = num;
                    ListeProjet.Add(projet);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur AjouterProjet : " + ex.Message);
                return false;
            }
        }

       
        private string RecupererNumeroProjet(int idProjet)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT numero_projet FROM projet WHERE numero_projet = @id",
                        conn);

                    cmd.Parameters.AddWithValue("@id", idProjet);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return result.ToString();
                }
            }
            catch { }

            return "";
        }

        public bool ModifierProjet(Projet projet)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                using MySqlCommand cmd = new MySqlCommand("modifier_projet", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@i_numero_projet", projet.Num_projet);
                cmd.Parameters.AddWithValue("@i_titre", projet.Titre);
                cmd.Parameters.AddWithValue("@i_description", projet.Description);
                cmd.Parameters.AddWithValue("@i_budget", projet.Budget);
                cmd.Parameters.AddWithValue("@i_nb_employes_requis", projet.Nb_employe);
                cmd.Parameters.AddWithValue("@i_total_salaires", projet.Total_salaire);
                cmd.Parameters.AddWithValue("@i_id_client", projet.Id_client);
                cmd.Parameters.AddWithValue("@i_statut", projet.Statut);

                cmd.ExecuteNonQuery();

             
                var existant = ListeProjet.FirstOrDefault(p => p.Num_projet == projet.Num_projet);
                if (existant != null)
                {
                    int index = ListeProjet.IndexOf(existant);
                    ListeProjet[index] = projet;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur ModifierProjet : " + ex.Message);
                return false;
            }
        }

    }
}











