using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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

namespace Projet_Jeremy_Jay.Pages.Projet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherProjet : Page
    {

        public ObservableCollection<Classes.Projet> ListeProjet { get; set; }

        public AfficherProjet()
        {
            InitializeComponent();
            ListeProjet = new ObservableCollection<Classes.Projet>();
            ChargerProjet();
        }

        private void ChargerProjet()
        {
            SingletonProjet.getInstance().getAllProjet();
            ListeProjet.Clear();
            foreach (var m in SingletonProjet.getInstance().Liste)
                ListeProjet.Add(m);
        }

        private void btnModifierProjet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAjouterProjet_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterProjet));
        }
    }
}
