using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay.Classes
{
    public class Employe
    {
        string matricule;
        string prenom;
        string nom;
        DateOnly  date_naissance;
        string email;
        string adresse;
        DateOnly  date_embauche;
        double taux_horaire;
        string photo;  
        string statut;

        public Employe(string matricule, string prenom, string nom, DateOnly date_naissance,
                       string email, string adresse, DateOnly date_embauche,
                       double taux_horaire, string photo, string statut)
        {
            this.matricule = matricule;
            this.prenom = prenom;
            this.nom = nom;
            this.date_naissance = date_naissance;
            this.email = email;
            this.adresse = adresse;
            this.date_embauche = date_embauche;
            this.taux_horaire = taux_horaire;
            this.photo = photo;
            this.statut = statut;
        }

        public string Matricule { get => matricule; set => matricule = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public DateOnly Date_naissance { get => date_naissance; set => date_naissance = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateOnly Date_embauche { get => date_embauche; set => date_embauche = value; }
        public double Taux_horaire { get => taux_horaire; set => taux_horaire = value; }
        public string Photo { get => photo; set => photo = value; }  
        public string Statut { get => statut; set => statut = value; }

        public bool EstOccupe { get; set; } = false;

        public override string? ToString()
        {
            return base.ToString();
}
    
   }
   } 