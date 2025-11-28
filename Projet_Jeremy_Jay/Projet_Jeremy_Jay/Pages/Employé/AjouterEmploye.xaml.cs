using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Employé
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjouterEmploye : Page
    {
        public AjouterEmploye()
        {
            InitializeComponent();
        
        
        }



        private void btn_AJouter(object sender, RoutedEventArgs e)
        {

            bool valide = true; 

       
            errPrenom.Text = "";
            errNom.Text = "";
            errEmail.Text = "";
            errAdresse.Text = "";
            errTaux.Text = "";
            errPhoto.Text = "";


            string prenom = tbxPrenom.Text.Trim();
            string nom = tbxNom.Text.Trim();
            DateOnly date_naissance =
        DateOnly.FromDateTime(dpNaissance.SelectedDate.Value.DateTime);
            string email = tbxEmail.Text.Trim();
            string adresse = tbxAdresse.Text.Trim();
            DateOnly date_Embauche =
      DateOnly.FromDateTime(dpEmbauche.SelectedDate.Value.DateTime);
            double tauxHoraire = nbTauxHoraire.Value;
            string statut = (cbStatut.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string photo = tbxPhoto.Text.Trim();


            
            if (string.IsNullOrWhiteSpace(nom))
            {
                errNom.Text = "Veuillez entrer un nom.";
                valide = false;
            }
            else if (nom.Length < 3 || nom.Length > 100)
            {
                errNom.Text = "Le nom doit avoir entre 3 et 100 caractères.";
                valide = false;
            }
            if (string.IsNullOrWhiteSpace(prenom))
            {
                errPrenom.Text = "Veuillez entrer un prénom.";
                valide = false;
            }
            else if (nom.Length < 3 || nom.Length > 100)
            {
                errPrenom.Text = "Le prénom doit avoir entre 3 et 100 caractères.";
                valide = false;
            }


            if (string.IsNullOrWhiteSpace(email))
            {
                errEmail.Text = "Veuillez entrer un email.";
                valide = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                errEmail.Text = "Email invalide.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(statut))
            {
                errStatut.Text = "Veuillez sélectionner une catégorie.";
                valide = false;
            }

           
      
            if (string.IsNullOrWhiteSpace(photo))
            {
                errPhoto.Text = "Veuillez entrer une l'url d'un photo.";
                valide = false;
            }
            else if (!Uri.IsWellFormedUriString(photo, UriKind.Absolute))
            {
                errPhoto.Text = "Lien URL invalide.";
                valide = false;
            }


            if (double.IsNaN(tauxHoraire))
            {
                errTaux.Text = "Veuillez entrer un taux horraire comme 19,99.";
                valide = false;
            }




            if (valide)
            {


                SingletonEmploye.getInstance().ajouterEmploye(
                prenom, nom, date_naissance, email, adresse,
               date_Embauche, tauxHoraire, photo, statut
            );

         
            tbxPrenom.Text = "";
            tbxNom.Text = "";
            dpNaissance.Date = DateTimeOffset.Now;   
            tbxEmail.Text = "";
            tbxAdresse.Text = "";
            dpEmbauche.Date = DateTimeOffset.Now;   
            nbTauxHoraire.Value = 0;
            cbStatut.SelectedIndex = -1;
            tbxPhoto.Text = "";
        }

    }

        private void Button_Retour(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }



}

