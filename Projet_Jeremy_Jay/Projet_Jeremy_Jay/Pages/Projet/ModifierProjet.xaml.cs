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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Projet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifierProjet : Page
    {
 
            private Classes.Projet projetActuel;

            public ModifierProjet()
            {
                this.InitializeComponent();
            }

            protected override void OnNavigatedTo(NavigationEventArgs e)
            {
                base.OnNavigatedTo(e);

                projetActuel = e.Parameter as Classes.Projet;

                if (projetActuel == null)
                {
                    txtMessage.Text = "Erreur : aucun projet sélectionné.";
                    return;
                }

                ChargerClients();
                RemplirChamps();
            }

            private void ChargerClients()
            {
                try
                {
                    var singletonClient = SingletonClient.getInstance();
                    singletonClient.getAllClient();

                    cbClients.ItemsSource = singletonClient.Liste;
                    cbClients.DisplayMemberPath = "Nom";
                    cbClients.SelectedValuePath = "Id_client";
                }
                catch (Exception ex)
                {
                    txtMessage.Text = "Erreur lors du chargement des clients : " + ex.Message;
                }
            }

            private void RemplirChamps()
            {
                if (projetActuel == null) return;

                txtTitre.Text = projetActuel.Titre;
                txtDescription.Text = projetActuel.Description;
            dpDateDebut.Date = new DateTimeOffset(projetActuel.Date_debut);
            txtBudget.Text = projetActuel.Budget.ToString();
                nbEmployesRequis.Value = projetActuel.Nb_employe;

                cbClients.SelectedValue = projetActuel.Id_client;

                cbStatut.SelectedItem = cbStatut.Items
                    .OfType<ComboBoxItem>()
                    .FirstOrDefault(i => i.Content.ToString() == projetActuel.Statut);
            }

        private bool ValiderChamps()
        {
           
            errClient.Text = "";
            errTitre.Text = "";
            errDescription.Text = "";
            errDateDebut.Text = "";
            errBudget.Text = "";
            errNbEmployes.Text = "";
            errStatut.Text = "";
        

            bool estValide = true;

            
            if (string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                errTitre.Text = "Veuillez entrer un titre.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errDescription.Text = "Veuillez entrer une description.";
                estValide = false;
            }

            if (cbClients.SelectedItem == null)
            {
                errClient.Text = "Veuillez sélectionner un client.";
                estValide = false;
            }

            if (!dpDateDebut.Date.HasValue)
            {
                errDateDebut.Text = "Veuillez sélectionner une date de début.";
                estValide = false;
            }

            if (!double.TryParse(txtBudget.Text, out double budget))
            {
                errBudget.Text = "Le budget doit être un nombre valide.";
                estValide = false;
            }
            else if (budget < 100)
            {
                errBudget.Text = "Le budget doit être d'au moins 100 $.";
                estValide = false;
            }


            if (nbEmployesRequis.Value < 1 || nbEmployesRequis.Value > 5)
            {
                errNbEmployes.Text = "Le nombre d'employés requis doit être entre 1 et 5.";
                estValide = false;
            }

            
            if (cbStatut.SelectedItem == null)
            {
                errStatut.Text = "Veuillez sélectionner un statut.";
                estValide = false;
            }

            return estValide;
        }
        private async void BtnModifierProjet_Click(object sender, RoutedEventArgs e)
            {
                if (!ValiderChamps()) return;

                projetActuel.Titre = txtTitre.Text;
                projetActuel.Description = txtDescription.Text;
            DateTime? dateDebut = dpDateDebut.Date.HasValue ? dpDateDebut.Date.Value.DateTime : (DateTime?)null;
            projetActuel.Budget = double.Parse(txtBudget.Text);
                projetActuel.Nb_employe = (int)nbEmployesRequis.Value;
                projetActuel.Id_client = (int)cbClients.SelectedValue;
                projetActuel.Statut = (cbStatut.SelectedItem as ComboBoxItem)?.Content.ToString();

                bool success = SingletonProjet.getInstance().ModifierProjet(projetActuel);

                txtMessage.Text = success ? "Projet modifié avec succès !" : "Erreur lors de la modification du projet.";

                if (success)
                {
               
                    await Task.Delay(3000);
                    txtMessage.Text = "";
                }
            }

    }
}