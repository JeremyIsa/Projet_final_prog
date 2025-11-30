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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjouterClient : Page
    {
        public AjouterClient()
        {
            InitializeComponent();
        }
       

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;

            errNom.Text = "";
            errAdresse.Text = "";
            errTel.Text = "";
            errEmail.Text = "";

            string nom = tbxNom.Text.Trim();
            string adresse = tbxAdresse.Text.Trim();
            string num_tel = tbxTelephone.Text.Trim();
            string email = tbxEmail.Text.Trim();

            if(string.IsNullOrWhiteSpace(nom))
            {
                errNom.Text = "Le nom est obligatoire.";
                valide = false;
            }
            else if(nom.Length < 3 || nom.Length > 50)
            {
                errNom.Text = "Le nom doit avoir entre 3 et 100 caractères.";
                valide = false;
            }

            if(string.IsNullOrWhiteSpace(adresse))
            {
                errAdresse.Text = "L'adresse est obligatoire.";
                valide = false;
            }
            else if(adresse.Length < 10 || adresse.Length > 150)
            {
                errAdresse.Text = "L'adresse doit avoir entre 10 et 200 caractères.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(num_tel))
            {
                errTel.Text = "Le numéro de téléphone est obligatoire.";
                valide = false;
            }
            else if (!Regex.IsMatch(num_tel, @"^\d{3}-\d{3}-\d{4}$"))
            {
                errTel.Text = "Format invalide (ex: 123-456-7890)";
                valide = false;
            }

            if(string.IsNullOrWhiteSpace(email))
            {
                errEmail.Text = "L'email est obligatoire.";
                valide = false;
            }
            else if(email.Length < 10 || email.Length > 100)
            {
                errEmail.Text = "L'email doit avoir entre 10 et 100 caractères.";
                valide = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                errEmail.Text = "Email invalide.";
                valide = false;
            }

            if (valide)
            {
                SingletonClient.getInstance().ajouterClient(nom, adresse, num_tel, email);

                Frame.GoBack();
            }


        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}
