using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Jeremy_Jay.Classes
{
    public class Admin
    {

        private string nomUtilisateur;
        private string motDePasse;
        private Boolean estAdmin;

        public Admin(string nomUtilisateur, string motDePasse, bool estAdmin)
        {
            this.nomUtilisateur = nomUtilisateur;
            this.motDePasse = motDePasse;
            this.estAdmin = estAdmin;
        }

        public string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }
        public string MotDePasse { get => motDePasse; set => motDePasse = value; }
        public bool EstAdmin { get => estAdmin; set => estAdmin = value; }




        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
