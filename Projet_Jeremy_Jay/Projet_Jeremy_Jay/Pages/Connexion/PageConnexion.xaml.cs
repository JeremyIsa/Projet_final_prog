using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Projet_Jeremy_Jay.Classes;
using Projet_Jeremy_Jay.Pages.Employé;
using Projet_Jeremy_Jay.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Connexion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageConnexion : Page
    {
        public PageConnexion()
        {
            InitializeComponent();
        }

    
        private async void btnCreerAdmin_Click(object sender, RoutedEventArgs e)
        {
            TextBox tbUser = new() { PlaceholderText = "Nom d'utilisateur" };
            PasswordBox tbPass = new() { PlaceholderText = "Mot de passe" };

            StackPanel sp = new();
            sp.Children.Add(tbUser);
            sp.Children.Add(tbPass);

            ContentDialog cd = new()
            {
                Title = "Créer un administrateur",
                Content = sp,
                PrimaryButtonText = "Créer",
                CloseButtonText = "Annuler",
                XamlRoot = this.XamlRoot
            };

            var result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                bool ok = SingletonAdmin.getInstance().CreerAdmin(tbUser.Text.Trim(), tbPass.Password.Trim());

                await new ContentDialog
                {
                    Title = ok ? "Succès" : "Erreur",
                    Content = ok ? "Administrateur créé avec succès." : "Impossible de créer l'admin (déjà existant ou champ vide).",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = tbxUser.Text.Trim();
            string pass = tbxPass.Password.Trim();

            if (SingletonAdmin.getInstance().Connecter(user, pass))
            {
                // Redirection vers la page principale
                Frame.Navigate(typeof(AfficherEmploye));
            }
            else
            {
                errMsg.Text = "Nom d'utilisateur ou mot de passe incorrect.";
            }

        }
    }
}