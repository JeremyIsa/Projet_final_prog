using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
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

namespace Projet_Jeremy_Jay.Pages.Employé
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherEmploye : Page
    {

        public ObservableCollection<Classes.Employe> ListeEmploye { get; set; }

        public AfficherEmploye()
        {
            InitializeComponent();

            ListeEmploye = new ObservableCollection<Classes.Employe>();


            ChargerEmploye();
        
        }

        private void ChargerEmploye()
        {
            SingletonEmploye.getInstance().getAllEmploye();

            ListeEmploye.Clear();

            foreach (var m in SingletonEmploye.getInstance().Liste)
                ListeEmploye.Add(m);
        }

     
            private void Button_Modifier(object sender, RoutedEventArgs e)
        {
            var bouton = sender as Button;
            var employe = bouton.DataContext as Classes.Employe;

          
            Frame.Navigate(typeof(ModifierEmploye));
        }

    



        private void btnRedirection_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterEmploye));
        }

      
    }
}
