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
    class SingletonClient
    {

        string connectionString;
        ObservableCollection<Client> ListeClient;

        static SingletonClient instance = null;

        private SingletonClient()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq2;Uid=6235801;Pwd=6235801;";
            ListeClient = new ObservableCollection<Client>();
        }

        public static SingletonClient getInstance()
        {
            if (instance == null)
                instance = new SingletonClient();
            return instance;
        }

        public ObservableCollection<Client> Liste => ListeClient;

        public void getAllClient()
        {
            ListeClient.Clear();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("liste_clients", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                using MySqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id_client = r.GetInt32("id_client");
                    string nom = r.GetString("nom");
                    string adresse = r.GetString("adresse");
                    string num_tel = r.GetString("telephone");
                    string email = r.GetString("email");


                    Client c = new Client(id_client, nom, adresse, num_tel, email);
                    ListeClient.Add(c);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ajouterClient(string nom, string adresse, string num_tel, string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("ajout_client", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@i_nom", nom);
                cmd.Parameters.AddWithValue("@i_adresse", adresse);
                cmd.Parameters.AddWithValue("@i_telephone", num_tel);
                cmd.Parameters.AddWithValue("@i_email", email);

                con.Open();
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = @"
                    SELECT id_client 
                    FROM client 
                    WHERE nom = @nom AND telephone = @telephone 
                    ORDER BY id_client DESC
                    LIMIT 1";

                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@telephone", num_tel);

                int id_client = Convert.ToInt32(cmd.ExecuteScalar());

                Client c = new Client(id_client, nom, adresse, num_tel, email);

                ListeClient.Add(c);
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void modifierClient(Client c)
        {

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand("modifier_client", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@i_id", c.Id_client);
                cmd.Parameters.AddWithValue("@i_nom", c.Nom);
                cmd.Parameters.AddWithValue("@i_adresse", c.Adresse);
                cmd.Parameters.AddWithValue("@i_telephone", c.Num_tel);
                cmd.Parameters.AddWithValue("@i_email", c.Email);

                con.Open();
                cmd.ExecuteNonQuery();

                getAllClient();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }



        }
    }
}   
