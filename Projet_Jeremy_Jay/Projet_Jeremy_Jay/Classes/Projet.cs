using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay.Classes
{
    class Projet
    {
        string num_projet;
        int id_client;
        string titre;
        DateTime date_debut;
        string description;
        double budget;
        int nb_employe;
        double total_salaire;
        int statut;

        public Projet(string num_projet, int id_client, string titre, DateTime date_debut, string description, double budget, int nb_employe, double total_salaire, int statut)
        {
            this.num_projet = num_projet;
            this.id_client = id_client;
            this.titre = titre;
            this.date_debut = date_debut;
            this.description = description;
            this.budget = budget;
            this.nb_employe = nb_employe;
            this.total_salaire = total_salaire;
            this.statut = statut;
        }

        public string Num_projet { get => num_projet; set => num_projet = value; }
        public int Id_client { get => id_client; set => id_client = value; }
        public string Titre { get => titre; set => titre = value; }
        public DateTime Date_debut { get => date_debut; set => date_debut = value; }
        public string Description { get => description; set => description = value; }
        public double Budget { get => budget; set => budget = value; }
        public int Nb_employe { get => nb_employe; set => nb_employe = value; }
        public double Total_salaire { get => total_salaire; set => total_salaire = value; }
        public int Statut { get => statut; set => statut = value; }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
