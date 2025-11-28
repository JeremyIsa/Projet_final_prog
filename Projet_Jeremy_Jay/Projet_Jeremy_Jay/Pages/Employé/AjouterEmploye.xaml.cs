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
using Windows.Foundation;
using Windows.Foundation.Collections;

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
            string prenom = tbxPrenom.Text;
            string nom = tbxNom.Text;
            DateTime dateNaissance = dpNaissance.Date.DateTime;
            string email = tbxEmail.Text;
            string adresse = tbxAdresse.Text;
            DateTime dateEmbauche = dpEmbauche.Date.DateTime;
            double tauxHoraire = nbTauxHoraire.Value;
            string statut = (cbStatut.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string photo = tbxPhoto.Text;

        
            SingletonEmploye.getInstance().ajouterEmploye(
                prenom, nom, dateNaissance, email, adresse,
                dateEmbauche, tauxHoraire, photo, statut
            );

         
            tbxPrenom.Text = "";
            tbxNom.Text = "";
            dpNaissance.Date = DateTimeOffset.Now;   // ou null si autorisé
            tbxEmail.Text = "";
            tbxAdresse.Text = "";
            dpEmbauche.Date = DateTimeOffset.Now;   // ou null
            nbTauxHoraire.Value = 0;
            cbStatut.SelectedIndex = -1;
            tbxPhoto.Text = "";
        }

    }
    }

  
