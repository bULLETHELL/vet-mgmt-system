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
        TextBox tbPatientName = new TextBox();
        TextBox tbPatientAge = new TextBox();
        TextBox tbPatientAnimal = new TextBox();
        TextBox tbTreatmentName = new TextBox();
        TextBox tbTreatmentPrice = new TextBox();
        ComboBox cbOwners = new ComboBox();
        ComboBox cbTreatments = new ComboBox();
        List<Owner> ownersList = new List<Owner>();
        List<Owner> uOwnersList = new List<Owner>();
        #endregion

        #region Owner Creation
        public void UserMgmtCreate_Click(object sender, RoutedEventArgs e)
        {
            // Clear stackpanel
            spMainWindow.Children.Clear();

            Button createButton = new Button() { Content = "Create Owner" };
            createButton.Click += OwnerCreateButton_Click;

            spMainWindow.Children.Add(new Label { Content = "First Name" });
            spMainWindow.Children.Add(tbFirstName);
            spMainWindow.Children.Add(new Label { Content = "Last Name" });
            spMainWindow.Children.Add(tbLastName);
            spMainWindow.Children.Add(new Label { Content = "Zip Code" });
            spMainWindow.Children.Add(tbZipCode);
            spMainWindow.Children.Add(new Label { Content = "City" });
            spMainWindow.Children.Add(tbCity);
            spMainWindow.Children.Add(new Label { Content = "Street Name" });
            spMainWindow.Children.Add(tbStreetName);
            spMainWindow.Children.Add(new Label { Content = "Street Number" });
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
                    popup.HorizontalAlignment = HorizontalAlignment.Center;
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
            Button deleteButton = new Button { Content = "Delete Selected Owner"};
            deleteButton.Click += DeleteOwnerButton_Click;
            using (var context = new VetMgmtSystemDbEntities()) 
            {
                foreach (Owner owner in context.Owners)
                {
                    if (owner.FirstName == tbFirstName.Text && owner.LastName == tbLastName.Text)
                    {
                        cbOwners.Items.Add(new ComboBoxItem { Content = $"{owner.OwnerID}. {owner.FirstName} {owner.LastName} {owner.Address.StreetName} {owner.Address.StreetNo}" });
                    }
                }
            }

            spMainWindow.Children.Add(cbOwners);
            spMainWindow.Children.Add(deleteButton);
        }

        private void DeleteOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbOwners.SelectedItem != null)
            {
                string trimmedOwnerString = cbOwners.SelectedItem.ToString().Remove(0, 38);
                string stringOwnerId = trimmedOwnerString.Remove(trimmedOwnerString.IndexOf('.'));
                int ownerId = Convert.ToInt32(stringOwnerId);

                using (var context = new VetMgmtSystemDbEntities())
                {
                    var owner = context.Owners.Find(ownerId);
                    var patient = context.Patients.FirstOrDefault(p => p.OwnerID == ownerId);
                    var patientTreatment = context.PatientsMedicalProcedures.FirstOrDefault(t => t.PatientID == patient.PatientID);

                    context.PatientsMedicalProcedures.Remove(patientTreatment);
                    context.SaveChanges();

                    context.Patients.Remove(patient);
                    context.SaveChanges();

                    context.Owners.Remove(owner);
                    context.SaveChanges();
                }
            }
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

            //  Clear the stackpanel
            spMainWindow.Children.Clear();

            Button createButton = new Button { Content = "Create Patient" };
            createButton.Click += PatientCreateButton_Click;

            spMainWindow.Children.Add(new Label { Content = "Patient Name" });
            spMainWindow.Children.Add(tbPatientName);
            spMainWindow.Children.Add(new Label { Content = "Patient Age" });
            spMainWindow.Children.Add(tbPatientAge);
            spMainWindow.Children.Add(new Label { Content = "Animal's Type" });
            spMainWindow.Children.Add(tbPatientAnimal);

            using (var context = new VetMgmtSystemDbEntities())
            {
                foreach (Owner owner in context.Owners)
                {
                    cbOwners.Items.Add(new ComboBoxItem { Content = $"{owner.OwnerID}. {owner.FirstName} {owner.LastName}" });
                }

                foreach (MedicalProcedure treatment in context.MedicalProcedures)
                {
                    cbTreatments.Items.Add(new ComboBoxItem { Content = $"{treatment.MedicalProcedureID}. {treatment.Name} {treatment.Price}" });
                }
            }
            spMainWindow.Children.Add(new Label { Content = "Choose the Patient's Owner" });
            spMainWindow.Children.Add(cbOwners);

            spMainWindow.Children.Add(new Label { Content = "Choose the Patient's Treatment" });
            spMainWindow.Children.Add(cbTreatments);
            spMainWindow.Children.Add(createButton);
        }

        private void PatientCreateButton_Click(object sender, RoutedEventArgs e)
        {
            string trimmedOwnerString = cbOwners.SelectedItem.ToString().Remove(0, 38);
            string ownerId = trimmedOwnerString.Remove(trimmedOwnerString.IndexOf('.'));

            string trimmedTreatmentString = cbTreatments.SelectedItem.ToString().Remove(0, 38);
            string treatmentId = trimmedTreatmentString.Remove(trimmedTreatmentString.IndexOf('.'));

            using (var context = new VetMgmtSystemDbEntities())
            {
                if (!context.Animals.Any(a => a.AnimalName == tbPatientAnimal.Text))
                {
                    var date = new Date
                    {
                        Date1 = DateTime.Today
                    };

                    context.Dates.Add(date);

                    var animal = new Animal
                    {
                        AnimalName = tbPatientAnimal.Text
                    };

                    context.Animals.Add(animal);

                    var patient = new Patient
                    {
                        Name = tbPatientName.Text,
                        Age = Convert.ToInt32(tbPatientAge.Text),
                        OwnerID = Convert.ToInt32(ownerId),
                        AnimalID = animal.AnimalID,
                        MedicalProcedureID = Convert.ToInt32(treatmentId)
                    };

                    context.Patients.Add(patient);

                    context.SaveChanges();
                }
            }
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
            spMainWindow.Children.Clear();

            Button createButton = new Button { Content = "Create Treatment"};
            createButton.Click += CreateTreatmentButton_Click;

            spMainWindow.Children.Add(new Label { Content = "Treatment Name" });
            spMainWindow.Children.Add(tbTreatmentName);
            spMainWindow.Children.Add(new Label { Content = "Treatment Price" });
            spMainWindow.Children.Add(tbTreatmentPrice);
            spMainWindow.Children.Add(createButton);
        }

        private void CreateTreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new VetMgmtSystemDbEntities())
            {
                if (!context.MedicalProcedures.Any(m => m.Name == tbTreatmentName.Text))
                {
                    var treatment = new MedicalProcedure
                    {
                        Name = tbTreatmentName.Text,
                        Price = Convert.ToDouble(tbTreatmentPrice.Text)
                    };

                    context.MedicalProcedures.Add(treatment);

                    context.SaveChanges();
                }
            }
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
            spMainWindow.Children.Clear();

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

