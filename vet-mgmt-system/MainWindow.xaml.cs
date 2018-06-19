using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;

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

        public TextBox tbOwnerName = new TextBox();
        TextBox tbFirstName = new TextBox();
        TextBox tbLastName = new TextBox();
        TextBox tbZipCode = new TextBox();
        TextBox tbCity = new TextBox();
        TextBox tbStreetName = new TextBox();
        TextBox tbStreetNo = new TextBox();
        StackPanel spInvoices = new StackPanel();
        StackPanel spUserCreate = new StackPanel { Orientation = Orientation.Vertical };

        public void UserMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button createButton = new Button() { Content = "Create" };
            createButton.Click += CreateButton_Click;

            spMainWindow.Children.Add(new Label { Content = "First Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbFirstName);
            spMainWindow.Children.Add(new Label { Content = "Last Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbLastName);
            spMainWindow.Children.Add(new Label { Content = "Zip Code", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbZipCode);
            spMainWindow.Children.Add(new Label { Content = "City", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbCity);
            spMainWindow.Children.Add(new Label { Content = "Street Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbStreetName);
            spMainWindow.Children.Add(new Label { Content = "Street Number", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbStreetNo);
            spMainWindow.Children.Add(createButton);
        }

        public void UserMgmtDelete_Click(object sender, RoutedEventArgs e)
        {
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button deleteButton = new Button() { Content = "Delete" };
            deleteButton.Click += DeleteButton_Click;

            spMainWindow.Children.Add(new Label { Content = "First Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbFirstName);
            spMainWindow.Children.Add(new Label { Content = "Last Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbLastName);
            spMainWindow.Children.Add(deleteButton);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: IMPLEMENT THIS FUNCTION
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //  Using the database
            using (var context = new VetMgmtSystemDbEntities())
            {
                //  Check if address and zipCity already exists in the database to avoid redundant data and FK errors
                if (!context.Addresses.Any(a => a.StreetName == tbStreetName.Text && a.StreetNo == tbStreetNo.Text) && !context.ZipCities.Any(z => z.ZipCode == Convert.ToInt32(tbZipCode.Text) && z.City == tbCity.Text))
                {
                    //  Make new instance of Address object
                    var address = new Address()
                    {
                        StreetName = tbStreetName.Text,
                        StreetNo = tbStreetNo.Text
                    };

                    //  Add address object to addresses table
                    context.Addresses.Add(address);

                    //  Make instance of ZipCity object
                    var zipCity = new ZipCity()
                    {
                        ZipCode = Convert.ToInt32(tbZipCode.Text),
                        City = tbCity.Text
                    };

                    //  Add ZipCity object to ZipCities table
                    context.ZipCities.Add(zipCity);

                    //  Make instance of owner object
                    var owner = new Owner()
                    {
                        FirstName = tbFirstName.Text,
                        LastName = tbLastName.Text,
                        ZipCode = Convert.ToInt32(tbZipCode.Text),
                        AddressID = address.AddressID
                    };

                    //  Add owner object to owners table
                    context.Owners.Add(owner);

                    //  Save changes to DB
                    context.SaveChanges();
                }
                else
                {
                    // Create popup
                    var popup = new Popup();
                    var dockPanel = new DockPanel();
                    popup.Child = dockPanel;
                    dockPanel.Children.Add(new TextBox { Text = "First Child" });
                    popup.IsOpen = true;
                }
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
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button searchInvoiceButton = new Button() { Content = "Create Invoice", HorizontalAlignment = HorizontalAlignment.Center };
            searchInvoiceButton.Click += SearchInvoiceButton_Click;

            spMainWindow.Children.Add(new Label { Content = "Search Owners", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbOwnerName);
            spMainWindow.Children.Add(searchInvoiceButton);
        }

        private void SearchInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewInvoicesWindow viewInvoicesWindow = new ViewInvoicesWindow();
            viewInvoicesWindow.Show();
        }
    }
}
