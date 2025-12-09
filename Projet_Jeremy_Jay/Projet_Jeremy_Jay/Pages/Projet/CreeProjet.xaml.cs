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
                cbClients.SelectedValuePath = "Id_client";
            }
            catch (Exception ex)
            {
                txtMessage.Text = "Erreur lors du chargement des clients : " + ex.Message;
            }
        }


        private void BtnCreerProjet_Click(object sender, RoutedEventArgs e)
        {
            errClient.Text = "";
            errTitre.Text = "";
            errDescription.Text = "";
            errDateDebut.Text = "";
            errBudget.Text = "";
            errNbEmployes.Text = "";

            bool valide = true;

            if (cbClients.SelectedValue == null)
            {
                errClient.Text = "Veuillez sélectionner un client.";
                valide = false;
            }

            string titre = txtTitre.Text.Trim();
            string description = txtDescription.Text.Trim();
            DateTime? dateDebut = dpDateDebut.Date?.DateTime;
            string budgetStr = txtBudget.Text.Trim();

            if (string.IsNullOrWhiteSpace(titre))
            {
                errTitre.Text = "Veuillez entrer un titre.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                errDescription.Text = "Veuillez entrer une description.";
                valide = false;
            }

            if (dateDebut == null)
            {
                errDateDebut.Text = "Veuillez choisir une date de début.";
                valide = false;
            }
            else if (dateDebut > DateTime.Today.AddYears(1))
            {
                errDateDebut.Text = "La date ne peut pas dépasser 1 an dans le futur.";
                valide = false;
            }

            if (!double.TryParse(budgetStr, out double budget))
            {
                errBudget.Text = "Budget invalide.";
                valide = false;
            }
            else if (budget < 100)
            {
                errBudget.Text = "Le budget doit être au minimum de 100$.";
                valide = false;
            }

            int nbEmployes = (int)nbEmployesRequis.Value;

            if (nbEmployes < 1 || nbEmployes > 5)
            {
                errNbEmployes.Text = "Le nombre d’employés doit être entre 1 et 5.";
                valide = false;
            }

            if (!valide)
                return;

            Classes.Projet p = new Classes.Projet(
                num_projet: "", 
                titre: titre,
                date_debut: dateDebut.Value,
                description: description,
                budget: budget,
                nb_employe: nbEmployes,
                total_salaire: 0,
                id_client: (int)cbClients.SelectedValue,
                statut: "En cours"
            );

            bool ok = SingletonProjet.getInstance().AjouterProjet(p);

            if (ok)
                txtMessage.Text = "Projet ajouté avec succès!";
            else
                txtMessage.Text = "Erreur lors de l'ajout du projet.";
        }
    }
}