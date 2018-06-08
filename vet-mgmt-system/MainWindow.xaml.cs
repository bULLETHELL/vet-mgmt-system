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
        Window DeleteUserWindow = new Window();
        DataGrid DeleteableUsersDG = new DataGrid();
        TextBox firstNameTextBox = new TextBox();
        TextBox lastNameTextBox = new TextBox();
        TextBox zipCodeTextBox = new TextBox();
        TextBox addressIDTextBox = new TextBox();

        public void UserMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            Window createUserWindow = new Window();
            createUserWindow.Width = 200;
            createUserWindow.Show();
            Button createButton = new Button() { Content = "Create" };
            createButton.Click += CreateButton_Click;
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };

            stackPanel.Children.Add(new Label { Content = "First Name", HorizontalAlignment = HorizontalAlignment.Center });
            stackPanel.Children.Add(firstNameTextBox);
            stackPanel.Children.Add(new Label { Content = "Last Name", HorizontalAlignment = HorizontalAlignment.Center });
            stackPanel.Children.Add(lastNameTextBox);
            stackPanel.Children.Add(new Label { Content = "Zip Code", HorizontalAlignment = HorizontalAlignment.Center });
            stackPanel.Children.Add(zipCodeTextBox);
            stackPanel.Children.Add(new Label { Content = "Address ID", HorizontalAlignment = HorizontalAlignment.Center });
            stackPanel.Children.Add(addressIDTextBox);
            stackPanel.Children.Add(createButton);
            createUserWindow.Content = stackPanel;
        }
        private void UserMgmtDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteUserWindow.Height = 400;
            DeleteUserWindow.Width = 200;
            DeleteUserWindow.Show();
            Button deleteButton = new Button() { Content = "Delete" };
            Button SearchButton = new Button() { Content = "Search" };
            deleteButton.Click += DeleteButton_Click;
            SearchButton.Click += SearchButton_Click;
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            stackPanel.Children.Add(new Label { Content = "Search for a User" });
            //stackPanel.Children.Add(new Label { Content = "only delete a user if the user has no patients registered in the system" });
            stackPanel.Children.Add(new TextBox { });
            stackPanel.Children.Add(SearchButton);
            stackPanel.Children.Add(DeleteableUsersDG);
            stackPanel.Children.Add(deleteButton);
            DeleteUserWindow.Content = stackPanel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new VetMgmtSystemDbEntities())
            {
                List<DeletableUser> list = new List<DeletableUser>();
                var dg = new DataGrid();
                list = context.DeletableUsers.ToList();
                dg.ItemsSource = list;
                DeleteableUsersDG = dg;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //  Using the database
            using (var context = new VetMgmtSystemDbEntities())
            {
                //  Make instance of owner object
                var owner = new Owner()
                {
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    ZipCode = Convert.ToInt32(zipCodeTextBox.Text),
                    AddressID = Convert.ToInt32(addressIDTextBox.Text)
                };

                //  Add owner object to owners table
                context.Owners.Add(owner);

                //  Save changes to DB
                context.SaveChanges();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
