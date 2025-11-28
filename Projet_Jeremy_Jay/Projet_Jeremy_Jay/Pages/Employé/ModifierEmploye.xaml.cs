using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Projet_Jeremy_Jay;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Employé
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierEmploye : Page
    {

        private Employe employeSelectionne;
        public ModifierEmploye()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            employeSelectionne = e.Parameter as Employe;

            if (employeSelectionne != null)
                RemplirChamps();
        }


        private void RemplirChamps()
        {
            tbxPrenom.Text = employeSelectionne.Prenom;
            tbxNom.Text = employeSelectionne.Nom;
            tbxPhoto.Text = employeSelectionne.Photo;
            tbxEmail.Text = employeSelectionne.Email;
            tbxAdresse.Text = employeSelectionne.Adresse;

            nbTauxHoraire.Value = employeSelectionne.Taux_horaire;

       
            try
            {
                Photo.Source = new BitmapImage(new Uri(employeSelectionne.Photo));
            }
            catch { }


            cbStatut.SelectedItem = cbStatut.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(i => (string)i.Content == employeSelectionne.Statut);
        }


        private bool ValiderChamps()
        {
            bool valide = true;

            errPrenom.Text = "";
            errNom.Text = "";
            errEmail.Text = "";
            errAdresse.Text = "";
            errStatut.Text = "";
            errTaux.Text = "";
            errPhoto.Text = "";

            string prenom = tbxPrenom.Text.Trim();
            string nom = tbxNom.Text.Trim();
            string email = tbxEmail.Text.Trim();
            string adresse = tbxAdresse.Text.Trim();
            string photo = tbxPhoto.Text.Trim();
            double? tauxHoraire = nbTauxHoraire.Value;
            string statut = (cbStatut.SelectedItem as ComboBoxItem)?.Content?.ToString();

        
            if (string.IsNullOrWhiteSpace(nom))
            {
                errNom.Text = "Veuillez entrer un nom.";
                valide = false;
            }
            else if (nom.Length < 3 || nom.Length > 30)
            {
                errNom.Text = "Le nom doit avoir entre 3 et 30 caractères.";
                valide = false;
            }

           
            if (string.IsNullOrWhiteSpace(prenom))
            {
                errPrenom.Text = "Veuillez entrer un prénom.";
                valide = false;
            }
            else if (prenom.Length < 3 || prenom.Length > 30)
            {
                errPrenom.Text = "Le prénom doit avoir entre 3 et 30 caractères.";
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

            if (string.IsNullOrWhiteSpace(adresse))
            {
                errAdresse.Text = "Veuillez entrer une adresse.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(statut))
            {
                errStatut.Text = "Veuillez sélectionner une catégorie.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(photo))
            {
                errPhoto.Text = "Veuillez entrer l'URL d'une photo.";
                valide = false;
            }
            else if (!Uri.IsWellFormedUriString(photo, UriKind.Absolute))
            {
                errPhoto.Text = "Lien URL invalide.";
                valide = false;
            }

        
            if (tauxHoraire == null || double.IsNaN(tauxHoraire.Value))
            {
                errTaux.Text = "Veuillez entrer un taux horaire valide.";
                valide = false;
            }

            return valide;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (!ValiderChamps())
                return;

            SingletonEmploye.getInstance().ModifierEmploye(
                employeSelectionne.Matricule,
                tbxPrenom.Text,
                tbxNom.Text,
                employeSelectionne.Date_naissance,
                tbxEmail.Text,
                tbxAdresse.Text,
                employeSelectionne.Date_embauche,
                nbTauxHoraire.Value,
                tbxPhoto.Text,
                (cbStatut.SelectedItem as ComboBoxItem)?.Content.ToString()
            );

            Frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }



    }
}
