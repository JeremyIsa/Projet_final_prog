using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay.Classes
{
    class Client
    {

        int id_client;
        string prenom;
        string nom;
        string adresse;
        string num_tel;
        string email;

        public Client(int id_client, string prenom, string nom, string adresse, string num_tel, string email)
        {
            this.id_client = id_client;
            this.prenom = prenom;
            this.nom = nom;
            this.adresse = adresse;
            this.num_tel = num_tel;
            this.email = email;
        }

        public int Id_client { get => id_client; set => id_client = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Num_tel { get => num_tel; set => num_tel = value; }
        public string Email { get => email; set => email = value; }

        public override string ToString()
        {
            return $"\nID du client : {id_client}" +
                   $"\nPrénom : {prenom}" +
                   $"\nNom : {nom}" +
                   $"\nAdresse : {adresse}" +
                   $"\nNuméro de téléphone : {num_tel}" +
                   $"\nEmail : {email}";
        }
    }
}
