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
                using MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM client";

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



    }
}
