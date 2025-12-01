using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Projet_Jeremy_Jay.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierClient : Page
    {
        private Classes.Client clientSelectionne;
        public ModifierClient()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            clientSelectionne = e.Parameter as Classes.Client;
            System.Diagnostics.Debug.WriteLine("Client sélectionné pour modification : " + clientSelectionne);
            if (clientSelectionne != null)
                RemplirChamps();
        }

        private void RemplirChamps()
        {
            if (clientSelectionne == null)
                return;

            tbxNom.Text = clientSelectionne.Nom ?? "";
            tbxAdresse.Text = clientSelectionne.Adresse ?? "";
            tbxTelephone.Text = clientSelectionne.Num_tel ?? "";
            tbxEmail.Text = clientSelectionne.Email ?? "";
        }

        private bool ValiderChamps()
        {
            bool valide = true;

            errNom.Text = "";
            errTel.Text = "";
            errEmail.Text = "";
            errAdresse.Text = "";

            string nom = tbxNom.Text.Trim();
            string telephone = tbxTelephone.Text.Trim();
            string email = tbxEmail.Text.Trim();
            string adresse = tbxAdresse.Text.Trim();

            if (string.IsNullOrWhiteSpace(nom))
            {
                errNom.Text = "Veuillez entrer un nom.";
                valide = false;
            }
            else if (nom.Length < 3 || nom.Length > 50)
            {
                errNom.Text = "Le nom doit avoir entre 3 et 50 caractères.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(telephone))
            {
                errTel.Text = "Veuillez entrer un téléphone.";
                valide = false;
            }
            else if (telephone.Length < 10 || telephone.Length > 20)
            {
                errTel.Text = "Le téléphone doit avoir entre 10 et 20 caractères.";
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
            else if (adresse.Length < 5 || adresse.Length > 100)
            {
                errAdresse.Text = "L'adresse doit avoir entre 5 et 100 caractères.";
                valide = false;
            }

            return valide;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValiderChamps())
                return;

            // Mettre à jour l'objet client avec les valeurs du formulaire
            clientSelectionne.Nom = tbxNom.Text;
            clientSelectionne.Adresse = tbxAdresse.Text;
            clientSelectionne.Num_tel = tbxTelephone.Text;
            clientSelectionne.Email = tbxEmail.Text;

            // Sauvegarder en base de données
            SingletonClient.getInstance().modifierClient(clientSelectionne);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
