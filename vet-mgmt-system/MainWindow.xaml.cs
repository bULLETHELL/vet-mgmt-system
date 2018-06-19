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

        #region Variables
        public TextBox tbOwnerName = new TextBox();
        TextBox tbFirstName = new TextBox();
        TextBox tbLastName = new TextBox();
        TextBox tbZipCode = new TextBox();
        TextBox tbCity = new TextBox();
        TextBox tbStreetName = new TextBox();
        TextBox tbStreetNo = new TextBox();
        List<Owner> ownersList = new List<Owner>();
        List<Owner> uOwnersList = new List<Owner>();
        #endregion

        #region Owner Creation
        public void UserMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button createButton = new Button() { Content = "Create" };
            createButton.Click += OwnerCreateButton_Click;

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

        private void OwnerCreateButton_Click(object sender, RoutedEventArgs e)
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
        #endregion

        #region Owner Deletion
        public void UserMgmtDelete_Click(object sender, RoutedEventArgs e)
        {
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button searchButton = new Button() { Content = "Search" };
            searchButton.Click += OwnerSearchButton_Click;

            spMainWindow.Children.Add(new Label { Content = "First Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbFirstName);
            spMainWindow.Children.Add(new Label { Content = "Last Name", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbLastName);
            spMainWindow.Children.Add(searchButton);
        }

        private void OwnerSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new VetMgmtSystemDbEntities())
            {
                ownersList = context.Owners.ToList();
                uOwnersList = ownersList.FindAll(o => o.FirstName == tbFirstName.Text && o.LastName == tbLastName.Text);
                var lb = new ListBox();
                Button deleteOwnerButton = new Button() { Content = "Delete" };

                deleteOwnerButton.Click += DeleteOwnerButton_Click;

                foreach (Owner owner in uOwnersList)
                {
                    lb.Items.Add(new ListBoxItem() { Content = $"{owner.OwnerID} {owner.FirstName} {owner.LastName} {owner.Address.StreetName} {owner.Address.StreetNo}", Name = $"lbItem{owner.OwnerID}" });
                }

                spMainWindow.Children.Add(lb);
                spMainWindow.Children.Add(deleteOwnerButton);
            }
        }

        private void DeleteOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region View All Owners
        public void UserMgmtViewAll_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: FIX THIS(MAYBE VIEW INSTEAD)

            using (var context = new VetMgmtSystemDbEntities())
            {
                List<Owner> list = context.Owners.ToList();

                var dg = new DataGrid();

                dg.ItemsSource = list;
                spMainWindow.Children.Add(dg);
            }
        }
        #endregion

        #region Patient Creation
        public void PatientMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: IMPLEMENT THIS FUNCTION
            throw new NotImplementedException();
        }
        #endregion

        #region Patient Deletion
        public void PatientMgmtDelete_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: IMPLEMENT THIS FUNCTION
            throw new NotImplementedException();
        }
        #endregion

        #region View All Patient
        public void PatientMgmtViewAll_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: FIX THIS(MAYBE VIEW INSTEAD)
            using (var context = new VetMgmtSystemDbEntities())
            {
                List<Patient> list = context.Patients.ToList();

                var dg = new DataGrid();

                dg.ItemsSource = list;
                spMainWindow.Children.Add(dg);
            }
        }
        #endregion

        #region Treatment Creation
        public void TreatmentsCreate_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: IMPLEMENT THIS FUNCTION
            throw new NotImplementedException();
        }
        #endregion

        #region Treatment Deletion
        public void TreatmentsDelete_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: IMPLEMENT THIS FUNCTION
            throw new NotImplementedException();
        }
        #endregion

        #region View All Treatments
        public void TreatmentsViewAll_Click(object sender, RoutedEventArgs e)
        {
            //  TODO: FIX THIS(MAYBE VIEW INSTEAD)
            using (var context = new VetMgmtSystemDbEntities())
            {
                List<MedicalProcedure> list = context.MedicalProcedures.ToList();

                var dg = new DataGrid();

                dg.ItemsSource = list;
                spMainWindow.Children.Add(dg);
            }
        }
        #endregion

        #region Treatments view
        private void ViewTreatments_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new VetMgmtSystemDbEntities())
            {
                List<TreatmentHistory> list = new List<TreatmentHistory>();
                var dg = new DataGrid();

                list = context.TreatmentHistories.ToList();
                dg.ItemsSource = list;
                spMainWindow.Children.Add(dg);
            }
        }
        #endregion

        #region Invoice Creation
        private void CreateInvoices_Click(object sender, RoutedEventArgs e)
        {
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button createInvoiceButton = new Button() { Content = "Create Invoice", HorizontalAlignment = HorizontalAlignment.Center };
            createInvoiceButton.Click += CreateInvoiceButton_Click;

            spMainWindow.Children.Add(new Label { Content = "Search Owners", HorizontalAlignment = HorizontalAlignment.Center });
            spMainWindow.Children.Add(tbOwnerName);
            spMainWindow.Children.Add(createInvoiceButton);
        }

        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewInvoicesWindow viewInvoicesWindow = new ViewInvoicesWindow();
            viewInvoicesWindow.Show();
        }
        #endregion
    }
}
