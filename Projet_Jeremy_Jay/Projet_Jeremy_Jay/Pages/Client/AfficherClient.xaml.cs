using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using Projet_Jeremy_Jay.Pages.Employé;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay.Pages.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherClient : Page
    {
        public ObservableCollection<Classes.Client> ListeClient { get; set; }

        public AfficherClient()
        {
            InitializeComponent();
            ListeClient = new ObservableCollection<Classes.Client>();
            btnAjouter.IsEnabled = Singleton.SingletonAdmin.getInstance().EstAdminConnecte();

            ChargerClient();
        }

        private void ChargerClient()
        {
            SingletonClient.getInstance().getAllClient();
            ListeClient.Clear();
            foreach (var m in SingletonClient.getInstance().Liste)
                ListeClient.Add(m);
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterClient));
        }

        private async  void btnModifier_Click(object sender, RoutedEventArgs e)
        {


            if (!Singleton.SingletonAdmin.getInstance().EstAdminConnecte())
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Accès refusé",
                    Content = "Vous devez être connecté en tant qu'administrateur pour modifier un client.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                await dialog.ShowAsync();
                return;
            }



            var bouton = sender as Button;
            var client = bouton.DataContext as Classes.Client;

            Frame.Navigate(typeof(ModifierClient), client);
        }
    }
}
