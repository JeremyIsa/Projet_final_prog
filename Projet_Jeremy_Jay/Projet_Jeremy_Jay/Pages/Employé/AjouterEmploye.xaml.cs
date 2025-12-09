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
using System.Threading.Tasks;
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
            errDateNaissance.Text = "";
            errDateEmbauche.Text = "";
            errStatut.Text = "";

            string prenom = tbxPrenom.Text.Trim();
            string nom = tbxNom.Text.Trim();
            string email = tbxEmail.Text.Trim();
            string adresse = tbxAdresse.Text.Trim();
            string photo = tbxPhoto.Text.Trim();
            double tauxHoraire = nbTauxHoraire.Value;
            string statut = (cbStatut.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(prenom))
            {
                errPrenom.Text = "Veuillez entrer un prénom.";
                valide = false;
            }
            if (string.IsNullOrWhiteSpace(nom))
            {
                errNom.Text = "Veuillez entrer un nom.";
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
                errPhoto.Text = "Veuillez entrer une URL.";
                valide = false;
            }
            else if (!Uri.IsWellFormedUriString(photo, UriKind.Absolute))
            {
                errPhoto.Text = "Lien URL invalide.";
                valide = false;
            }

            if (double.IsNaN(tauxHoraire))
            {
                errTaux.Text = "Veuillez entrer un taux horaire valide.";
                valide = false;
            }
            else if (tauxHoraire < 18 || tauxHoraire > 60)
            {
                errTaux.Text = "Le taux horaire doit être entre 18$ et 60$.";
                valide = false;
            }

            if (dpNaissance.SelectedDate == null)
            {
                errDateNaissance.Text = "Veuillez choisir une date de naissance.";
                valide = false;
            }
            if (dpEmbauche.SelectedDate == null)
            {
                errDateEmbauche.Text = "Veuillez choisir une date d'embauche.";
                valide = false;
            }

            DateOnly date_naissance = default;
            DateOnly date_embauche = default;

            if (dpNaissance.SelectedDate != null)
                date_naissance = DateOnly.FromDateTime(dpNaissance.SelectedDate.Value.DateTime);
            if (dpEmbauche.SelectedDate != null)
                date_embauche = DateOnly.FromDateTime(dpEmbauche.SelectedDate.Value.DateTime);



            if (dpNaissance.SelectedDate != null && dpEmbauche.SelectedDate != null)
            {
                DateTime d1 = date_naissance.ToDateTime(new TimeOnly(0, 0));
                DateTime d2 = date_embauche.ToDateTime(new TimeOnly(0, 0));
                double ageEnAnnees = (d2 - d1).TotalDays / 365.25;

                if (ageEnAnnees < 18)
                {
                    errDateNaissance.Text = "L'employé doit avoir au moins 18 ans.";
                    valide = false;
                }
                else if (ageEnAnnees > 65)
                {
                    errDateNaissance.Text = "L'employé ne peut pas avoir plus de 65 ans.";
                    valide = false;
                }
            }

            if (string.IsNullOrWhiteSpace(adresse))
            {
                errAdresse.Text = "Veuillez entrer une adresse.";
                valide = false;
            }
            if (dpEmbauche.SelectedDate != null)
            {
                DateTime embauche = date_embauche.ToDateTime(new TimeOnly(0, 0));
                DateTime aujourdHui = DateTime.Today;

                if (embauche > aujourdHui)
                {
                    errDateEmbauche.Text = "La date d'embauche ne peut pas être dans le futur.";
                    valide = false;
                }

                if (embauche < aujourdHui.AddYears(-65))
                {
                    errDateEmbauche.Text = "La date d'embauche ne peut pas être antérieure de plus de 65 ans.";
                    valide = false;
                }
            }

            if (!valide) return;

            // AJOUT EN BD
            SingletonEmploye.getInstance().ajouterEmploye(
                prenom, nom, date_naissance, email, adresse,
                date_embauche, tauxHoraire, photo, statut
            );

            // MESSAGE SUCCÈS
            infoSuccess.IsOpen = true;
            var _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => infoSuccess.IsOpen = false);
            });

            // RESET FORMULAIRE
            tbxPrenom.Text = "";
            tbxNom.Text = "";
            tbxEmail.Text = "";
            tbxAdresse.Text = "";
            dpNaissance.Date = DateTimeOffset.Now;
            dpEmbauche.Date = DateTimeOffset.Now;
            nbTauxHoraire.Value = 0;
            cbStatut.SelectedIndex = -1;
            tbxPhoto.Text = "";
        }

        private void Button_Retour(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}


