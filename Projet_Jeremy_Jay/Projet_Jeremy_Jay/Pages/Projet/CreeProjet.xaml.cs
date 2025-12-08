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

namespace Projet_Jeremy_Jay.Pages.Projet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreeProjet : Page
    {
        public CreeProjet()
        {
            this.InitializeComponent();

            SingletonClient.getInstance().getAllClient();



            ChargerClients();
        }

        private void ChargerClients()
        {
            try
            {
                
                SingletonClient.getInstance().getAllClient();

               
                cbClients.ItemsSource = SingletonClient.getInstance().Liste;

                cbClients.DisplayMemberPath = "Nom";
                cbClients.SelectedValuePath = "Identifiant";
            }
            catch (Exception ex)
            {
                txtMessage.Text = "Erreur lors du chargement des clients : " + ex.Message;
            }
        }


        private void BtnCreerProjet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbClients.SelectedValue == null)
                {
                    txtMessage.Text = "Veuillez sélectionner un client.";
                    return;
                }

                int idClient = (int)cbClients.SelectedValue;

                string titre = txtTitre.Text.Trim();
                string description = txtDescription.Text.Trim();
                DateTime? dateDebut = dpDateDebut.Date?.DateTime;

                if (string.IsNullOrWhiteSpace(titre) ||
                    string.IsNullOrWhiteSpace(description) ||
                    dateDebut == null)
                {
                    txtMessage.Text = "Veuillez remplir correctement les champs.";
                    return;
                }

                if (!double.TryParse(txtBudget.Text, out double budget))
                {
                    txtMessage.Text = "Budget invalide.";
                    return;
                }

                int nbEmployes = (int)nbEmployesRequis.Value;

               Classes.Projet nouveauProjet = new Classes.Projet(
    null,
    titre,
    dateDebut.Value,
    description,
    budget,
    nbEmployes,
    0.0,          
    idClient,
    "En cours"
);

                bool ok = SingletonProjet.getInstance().AjouterProjet(nouveauProjet);

                if (ok)
                {
                    txtMessage.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Green);
                    txtMessage.Text = "Projet créé avec succès !";

                    txtTitre.Text = "";
                    txtDescription.Text = "";
                    txtBudget.Text = "";
                    dpDateDebut.Date = null;
                    nbEmployesRequis.Value = 1;
                }
                else
                {
                    txtMessage.Text = "Erreur : le projet n’a pas pu être créé.";
                }
            }
            catch (Exception ex)
            {
                txtMessage.Text = "Erreur : " + ex.Message;
            }
        }
    }
}