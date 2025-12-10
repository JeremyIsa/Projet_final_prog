using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Projet_Jeremy_Jay.Classes;
using Projet_Jeremy_Jay.Pages.Client;
using Projet_Jeremy_Jay.Pages.Connexion;
using Projet_Jeremy_Jay.Pages.Employé;
using Projet_Jeremy_Jay.Pages.Projet;
using Projet_Jeremy_Jay.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_Jeremy_Jay
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem item)
            {
                switch (item.Tag)
                {
                    case "clients":
                        mainFrame.Navigate(typeof(AfficherClient));
                        break;
                    case "employe":
                        mainFrame.Navigate(typeof(AfficherEmploye));
                        break;
                    case "projet":
                        mainFrame.Navigate(typeof(AfficherProjet));
                        break;
                    case "connexion":
                        mainFrame.Navigate(typeof(PageConnexion));
                        break;

                    case "deconnexion":
                        SingletonAdmin.getInstance().Deconnecter();
                        mainFrame.Navigate(typeof(AfficherEmploye));
                        break;




                    default:
                        break;


                }


            }
        }

        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (mainFrame.CanGoBack)
                mainFrame.GoBack();
        }

        private async void btnExporter_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
            picker.SuggestedFileName = "Laboratoire_final";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFicher = await picker.PickSaveFileAsync();

            List<Projet> liste = SingletonProjet.getInstance().Liste.ToList();

            if(monFicher != null)
                await Windows.Storage.FileIO.WriteLinesAsync(monFicher, liste.ConvertAll(x => x.stringCSV()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
