using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace vet_mgmt_system
{
    /// <summary>
    /// Interaction logic for ViewInvoicesWindow.xaml
    /// </summary>
    public partial class ViewInvoicesWindow : Window
    {
        MainWindow mainWindow = ((MainWindow)Application.Current.MainWindow);

        public ViewInvoicesWindow()
        {
            InitializeComponent();
        }

        private void ViewInvoicesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new VetMgmtSystemDbEntities())
            {
                // Variable instantiation
                var invoices = context.proc_GetInvoiceByOwnerName(mainWindow.tbOwnerName.Text);
                List<string> patients = new List<string>();
                List<string> treatments = new List<string>();
                List<string> prices = new List<string>();
                List<string> firstNames = new List<string>();
                List<string> lastNames = new List<string>();
                List<string> streetNames = new List<string>();
                List<string> streetNumbers = new List<string>();
                List<string> cities = new List<string>();
                List<string> zipCodes = new List<string>();

                // Pulling from database
                foreach (proc_GetInvoiceByOwnerName_Result1 invoice in invoices)
                {
                    patients.Add(invoice.PatientName);
                    treatments.Add(invoice.MedicalProcedureName);
                    prices.Add(invoice.Price.ToString());
                    firstNames.Add(invoice.FirstName);
                    lastNames.Add(invoice.LastName);
                    streetNames.Add(invoice.StreetName);
                    streetNumbers.Add(invoice.StreetNo);
                    cities.Add(invoice.City);
                    zipCodes.Add(invoice.ZipCode.ToString());                    
                }

                // Content
                this.Title = $"Invoice - {firstNames.FirstOrDefault()} {lastNames.FirstOrDefault()}";

                ownerNameLabel.Content = $"{firstNames.FirstOrDefault()} {lastNames.FirstOrDefault()}";
                ownerAddressLabel.Content = $"{streetNames.FirstOrDefault()} {streetNumbers.FirstOrDefault()}";
                ownerZipCityLabel.Content = $"{zipCodes.FirstOrDefault()} {cities.FirstOrDefault()}";
                dateLabel.Content = DateTime.Today;
                patientItemsControl.ItemsSource = patients;
                treatmentItemsControl.ItemsSource = treatments;
                priceItemsControl.ItemsSource = prices;
            }
        }
    }
}
