using ProjetDotNet.Configuration;
using ProjetDotNet.Database;
using ProjetDotNet.Model;
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

namespace ProjetDotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
       private readonly ApplicationContext _context = new ApplicationContext(Configuration.Configuration.connectionString);

        public MainWindow()
        {
            InitializeComponent();
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

            Investigation investigation = new Investigation
            {
                InvestigationNumber = (DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + "1"),
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



            _context.Add(complainant);
            _context.Add(investigator);
            _context.Add(suspect);
            _context.Add(investigation);
            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
