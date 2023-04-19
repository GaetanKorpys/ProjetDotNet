using CefSharp.Wpf;
using ProjetDotNet.Database;
using ProjetDotNet.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjetDotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public InvestigatorViewModel InvestigatorVM { get; set; }


        private WebBrowser _myWebBrowser;

        //private readonly ApplicationContext _context = new ApplicationContext(Configuration.Configuration.connectionString);

        public MainWindow()
        {
            InitializeComponent();
            InvestigatorVM = new InvestigatorViewModel();
            DataContext = InvestigatorVM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Complainant complainant = new Complainant
            {
                Name = "Joe",
                LastName = "Mert",
                Email = "test@email.com",
                PhoneNumber = "0600000000",
                City = "Metz",
                Country = "France",
                PostalCode = 57000,
                NumberAdress = 45,
                Street = "Rue Capucin"
            };

            Suspect suspect = new Suspect
            {
                City = "Metz",
                Country = "France",
                PostalCode = 57000,
                Street = "Rue Capucin"
            };


            Console.WriteLine(DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + "1");

            int numInvestigation = 1;

            Investigation investigation = new Investigation
            {
                InvestigationNumber = DateTime.Today.Year.ToString() + " " + DateTime.Today.Month.ToString() + " " + numInvestigation.ToString(),
                Suspect = suspect,
                Complainant = complainant,
                Reason = "Battue",
                NumberOfAnimals = 1,
                AnimalBreed = "Chien",
                InvestigationStartDate = DateTime.Today,
            };

            Investigator investigator = new Investigator
            {
                Name = "Paul",
                LastName = "Pol",
                Email = "test@email.com",
                PhoneNumber = "0600000088",
                City = "Metz",
                Country = "France",
                PostalCode = 57000,
                NumberAdress = 61,
                Street = "Rue Florent",
            };


            /*
            _context.Add(complainant);
            _context.Add(investigator);
            _context.Add(suspect);
            _context.Add(investigation);
            _context.SaveChanges();
            _context.Dispose();
            */


            /*
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-web-security", "1");

            MyWebView.FrameLoadEnd += MyWebView_FrameLoadEnd;
            MyWebView.LoadHtml("<html><body><iframe src="https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=Paris&destination=Marseille&avoid=tolls/" width="800" height="450" frameborder="0" style="border:0"></iframe></body></html>");
            */
        }

        private void cmbInvestigators_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Récupération de l'investigateur sélectionné dans la vue
            var selectedInvestigator = cmbInvestigators.SelectedItem as Investigator;

            if (selectedInvestigator == null)
            {
                InvestigatorVM.ClearInvestigatorFields();
                return;
            }

            // Mise à jour de la propriété SelectedInvestigator de votre ViewModel
            InvestigatorVM.SelectedInvestigator = selectedInvestigator;
            InvestigatorVM.FillTextBox();
        }

        private void cmbInvestigators_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox combobox = (sender as ComboBox);

              combobox.SelectedIndex = 0;
              combobox.SelectedValue = null;
              combobox.SelectedItem = null;
        }



        /*
        private void MyWebView_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                MyWebView.ExecuteScriptAsync("alert('Map loaded successfully');");
            }

        }
        */
    }
}
