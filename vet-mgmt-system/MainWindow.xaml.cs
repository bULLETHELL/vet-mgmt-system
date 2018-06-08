using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;

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

        private void ViewTreatments_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new VetMgmtSystemDbEntities())
            {
                Window viewTreatmentsWindow = new Window();
                List<TreatmentHistory> list = new List<TreatmentHistory>();
                var dg = new DataGrid();

                viewTreatmentsWindow.Show();

                list = context.TreatmentHistories.ToList();
                dg.ItemsSource = list;
                viewTreatmentsWindow.Content = dg;
            }
        }
    }
}
