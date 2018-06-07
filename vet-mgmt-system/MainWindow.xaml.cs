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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vet_mgmt_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            Window createUserWindow = new Window();
            createUserWindow.Width = 200;
            createUserWindow.Show();
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            stackPanel.Children.Add(new Label { Content = "Name" , HorizontalAlignment = HorizontalAlignment.Center});
            stackPanel.Children.Add(new TextBox());
            createUserWindow.Content = stackPanel;
        }
    }
}
