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
        public TextBox tbOwnerName = new TextBox();
        StackPanel spInvoices = new StackPanel();
        StackPanel spUserCreate = new StackPanel { Orientation = Orientation.Vertical };

        public void UserMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            Window createUserWindow = new Window();
            createUserWindow.Width = 200;
            createUserWindow.Show();
            Button createButton = new Button() { Content = "Create" };
            createButton.Click += CreateButton_Click;

            if (spUserCreate.Children.Count == 0)
            {
                spUserCreate.Children.Add(new Label { Content = "First Name", HorizontalAlignment = HorizontalAlignment.Center });
                spUserCreate.Children.Add(firstNameTextBox);
                spUserCreate.Children.Add(new Label { Content = "Last Name", HorizontalAlignment = HorizontalAlignment.Center });
                spUserCreate.Children.Add(lastNameTextBox);
                spUserCreate.Children.Add(new Label { Content = "Zip Code", HorizontalAlignment = HorizontalAlignment.Center });
                spUserCreate.Children.Add(zipCodeTextBox);
                spUserCreate.Children.Add(new Label { Content = "Address ID", HorizontalAlignment = HorizontalAlignment.Center });
                spUserCreate.Children.Add(addressIDTextBox);
                spUserCreate.Children.Add(createButton);
            }
            createUserWindow.Content = spUserCreate;
            createUserWindow.Closing += CreateUserWindow_Closing;
        }

        private void CreateUserWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Clear the stackpanel when window closes to avoid errors
            spUserCreate.Children.Clear();
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

        private void ViewInvoices_Click(object sender, RoutedEventArgs e)
        {
            Window createInvoicesWindow = new Window();
            Button searchInvoiceButton = new Button() { Content = "Create Invoice", HorizontalAlignment = HorizontalAlignment.Center};
            searchInvoiceButton.Click += SearchInvoiceButton_Click;
            createInvoicesWindow.Show();
            spInvoices.Children.Add(new Label { Content = "Search Owners", HorizontalAlignment = HorizontalAlignment.Center});
            spInvoices.Children.Add(tbOwnerName);
            spInvoices.Children.Add(searchInvoiceButton);
            createInvoicesWindow.Content = spInvoices;
        }

        private void SearchInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewInvoicesWindow viewInvoicesWindow = new ViewInvoicesWindow();
            viewInvoicesWindow.Show();
        }
    }
}
