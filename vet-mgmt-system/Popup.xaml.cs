using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vet_mgmt_system
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        public Popup(string title, string dialog)
        {
            InitializeComponent();

            //  Set the title
            this.Title = title;

            //  Center the popup on MainWindow
            this.Owner = Application.Current.MainWindow;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            //  Set the dialog
            this.lblPopupMessage.Content = dialog;

            //  Show the popup
            this.Show();
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
