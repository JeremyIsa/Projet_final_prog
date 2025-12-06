using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Projet_Jeremy_Jay.Classes;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI;
using MySql.Data.MySqlClient;   // ?? IMPORTANT pour le SELECT COUNT(*)

namespace Projet_Jeremy_Jay.Pages.Projet
{
    public sealed partial class AjouterProjet : Page
    {
        private SingletonProjet singletonProjet = SingletonProjet.getInstance();
        private SingletonEmploye singletonEmploye = SingletonEmploye.getInstance();

        public AjouterProjet()
        {
            this.InitializeComponent();

            lvEmployes.ContainerContentChanging += LvEmployes_ContainerContentChanging;

            ChargerProjets();
            ChargerEmployes();
        }

        // -----------------------------------------------------------
        // 1) Chargement des projets
        // -----------------------------------------------------------
        private void ChargerProjets()
        {
            try
            {
                singletonProjet.getAllProjet();
                cbProjets.ItemsSource = singletonProjet.Liste;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                errProjet.Text = "Impossible de charger les projets.";
            }
        }

        private void cbProjets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errProjet.Text = "";
            infoSuccess.IsOpen = false;
            MiseAJourBoutonAssigner();
        }

        private void MiseAJourBoutonAssigner()
        {
            if (cbProjets.SelectedItem is Classes.Projet p)
            {
                btnAssigner.IsEnabled = p.Statut == "En cours";
            }
            else
            {
                btnAssigner.IsEnabled = false;
            }
        }

        // -----------------------------------------------------------
        // 2) Chargement des employés
        // -----------------------------------------------------------
        private void ChargerEmployes()
        {
            try
            {
                singletonEmploye.getAllEmploye();
                var liste = singletonEmploye.Liste;

                foreach (var emp in liste)
                {
                    try
                    {
                        var projetsEmp = singletonProjet.obtenirProjetsPourEmploye(emp.Matricule);
                        emp.EstOccupe = projetsEmp.Any(pe => pe.Statut == "En cours");
                    }
                    catch
                    {
                        emp.EstOccupe = false;
                    }
                }

                lvEmployes.ItemsSource = liste;
            }
            catch
            {
                errEmployes.Text = "Impossible de charger les employés.";
            }
        }

        // -----------------------------------------------------------
        // 3) Coloration + désactivation employés déjà occupés
        // -----------------------------------------------------------
        private void LvEmployes_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            try
            {
                if (args.Item is Employe emp && args.ItemContainer is ListViewItem container)
                {
                    if (emp.EstOccupe)
                    {
                        container.Background = new SolidColorBrush(Color.FromArgb(255, 255, 160, 160));
                        container.IsEnabled = false;
                    }
                    else
                    {
                        container.Background = null;
                        container.IsEnabled = true;
                    }
                }
            }
            catch { }
        }

        // -----------------------------------------------------------
        // Fonction locale pour compter les employés déjà assignés
        // -----------------------------------------------------------
        private int GetNombreEmployesAssignes_Local(string numeroProjet)
        {
            int count = 0;

            string connectionString =
                "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq2;Uid=6235801;Pwd=6235801;";

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM Employe_Projet WHERE numero_projet = @num",
                    con);

                cmd.Parameters.AddWithValue("@num", numeroProjet);

                con.Open();
                object res = cmd.ExecuteScalar();
                count = Convert.ToInt32(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du comptage des employés assignés : " + ex.Message);
            }

            return count;
        }

        // -----------------------------------------------------------
        // 4) Bouton Assigner
        // -----------------------------------------------------------
        private void btnAssigner_Click(object sender, RoutedEventArgs e)
        {
            errProjet.Text = "";
            errEmployes.Text = "";
            infoSuccess.IsOpen = false;

            if (cbProjets.SelectedItem is not Classes.Projet projet)
            {
                errProjet.Text = "Veuillez sélectionner un projet.";
                return;
            }

            if (projet.Statut != "En cours")
            {
                errProjet.Text = "Le projet doit être 'En cours'.";
                return;
            }

            var selection = lvEmployes.SelectedItems.Cast<Employe>().Where(emp => !emp.EstOccupe).ToList();

            if (selection.Count == 0)
            {
                errEmployes.Text = "Aucun employé valide n'a été sélectionné.";
                return;
            }

            // ?? Vérification du nombre d'employés requis
            int dejaAssignes;
            try
            {
                dejaAssignes = GetNombreEmployesAssignes_Local(projet.Num_projet);
            }
            catch (Exception ex)
            {
                errEmployes.Text = "Impossible de vérifier les employés déjà assignés : " + ex.Message;
                return;
            }

            int restant = projet.Nb_employe - dejaAssignes;

            if (selection.Count > restant)
            {
                errEmployes.Text = $"Ce projet n'accepte plus que {restant} employé(s).";
                return;
            }

            // ?? Assignation en BD
            foreach (var emp in selection)
            {
                try
                {
                    singletonProjet.ajouterEmployeAuProjet(emp.Matricule, projet.Num_projet, 0.0, 0.0);
                }
                catch (Exception ex)
                {
                    errEmployes.Text = $"Erreur : {emp.Prenom} {emp.Nom} n'a pas pu être assigné ({ex.Message}).";
                    return;
                }
            }

            infoSuccess.Message = "Employé(s) assigné(s) avec succès.";
            infoSuccess.IsOpen = true;

            // Rafraîchir l'affichage
            ChargerProjets();
            ChargerEmployes();

            cbProjets.SelectedItem = singletonProjet.Liste.FirstOrDefault(p => p.Num_projet == projet.Num_projet);
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
