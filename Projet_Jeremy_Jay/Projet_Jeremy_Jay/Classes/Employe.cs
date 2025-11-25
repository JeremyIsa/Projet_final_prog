using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay.Classes
{
  public  class Employe
    {
        string matricule;
        string num_projet;
        string prenom;
        string nom;
        DateTime date_naissance;
        string email;
        string adresse;
        DateTime date_embauche;
        double taux_horaire;
        string photo_id;
        string statut;

        public Employe(string matricule, string num_projet, string prenom, string nom, DateTime date_naissance, string email, string adresse, DateTime date_embauche, double taux_horaire, string photo_id, string statut)
        {
            this.matricule = matricule;
            this.num_projet = num_projet;
            this.prenom = prenom;
            this.nom = nom;
            this.date_naissance = date_naissance;
            this.email = email;
            this.adresse = adresse;
            this.date_embauche = date_embauche;
            this.taux_horaire = taux_horaire;
            this.photo_id = photo_id;
            this.statut = statut;
        }

        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public DateTime Date_naissance { get => date_naissance; set => date_naissance = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime Date_embauche { get => date_embauche; set => date_embauche = value; }
        public double Taux_horaire { get => taux_horaire; set => taux_horaire = value; }
        public string Photo_id { get => photo_id; set => photo_id = value; }
        public string Statut { get => statut; set => statut = value; }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
