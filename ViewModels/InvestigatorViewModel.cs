using CommunityToolkit.Mvvm.Input;
using ProjetDotNet.Database;
using ProjetDotNet.Models;
using ProjetDotNet.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjetDotNet.ViewModels
{
    public class InvestigatorViewModel : BaseViewModel
    {

        private Investigator _selectedInvestigator;
        private string _name;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _city;
        private string _country;
        private int _postalCode;
        private int _numberAdress;
        private string _street;


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public int PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        public int NumberAdress
        {
            get { return _numberAdress; }
            set
            {
                _numberAdress = value;
                OnPropertyChanged(nameof(NumberAdress));
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                OnPropertyChanged(nameof(Street));
            }
        }


        public Investigator SelectedInvestigator
        {
            get { return _selectedInvestigator; }
            set
            {
                _selectedInvestigator = value;
                FillTextBox();
                OnPropertyChanged(nameof(SelectedInvestigator));
            }
        }

        public ObservableCollection<Investigator> Investigators { get; set; }

        public ICommand AddInvestigatorCommand { get; set; }
        public ICommand UpdateInvestigatorCommand { get; set; }
        public ICommand DeleteInvestigatorCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }



        // Constructor
        public InvestigatorViewModel()
        {
            AddInvestigatorCommand = new RelayCommand(AddInvestigator);
            UpdateInvestigatorCommand = new RelayCommand(UpdateInvestigator);
            DeleteInvestigatorCommand = new RelayCommand(DeleteInvestigator);
            ClearFieldsCommand = new RelayCommand(ClearInvestigatorFields);

            using (var context = new ApplicationContext())
            {
                var investigators = context.Investigators.ToList();
                Investigators = new ObservableCollection<Investigator>(investigators);
            }
        }

        private void AddInvestigator()
        {
            String erreur = CanAddInvestigator();
            if (erreur != "")
            {
                MessageBox.Show("Veuillez remplir tous les champs:\n" + erreur, "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigator = new Investigator()
                {
                    Name = Name,
                    LastName = LastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    City = City,
                    Country = Country,
                    PostalCode = PostalCode,
                    NumberAdress = NumberAdress,
                    Street = Street
                };

                db.Investigators.Add(investigator);
                db.SaveChanges();

                // Add the new investigator to the list of investigators
                Investigators.Add(investigator);

                ClearInvestigatorFields();
            }
        }

        private void UpdateInvestigator()
        {
            if (SelectedInvestigator == null)
            {
                MessageBox.Show("Veuillez sélectionner un inspecteur à modifier.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigatorToUpdate = db.Investigators.Find(SelectedInvestigator.InvestigatorId);

                if (investigatorToUpdate != null)
                {
                    investigatorToUpdate.Name = Name;
                    investigatorToUpdate.LastName = LastName;
                    investigatorToUpdate.Email = Email;
                    investigatorToUpdate.PhoneNumber = PhoneNumber;
                    investigatorToUpdate.City = City;
                    investigatorToUpdate.Country = Country;
                    investigatorToUpdate.PostalCode = PostalCode;
                    investigatorToUpdate.NumberAdress = NumberAdress;
                    investigatorToUpdate.Street = Street;

                    db.SaveChanges();
                }
                ClearInvestigatorFields();
            }
        }

        private void DeleteInvestigator()
        {
            if (SelectedInvestigator == null)
            {
                MessageBox.Show("Veuillez sélectionner un inspecteur à supprimer.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigatorToDelete = db.Investigators.Find(SelectedInvestigator.InvestigatorId);

                if (investigatorToDelete != null)
                {
                    db.Investigators.Remove(investigatorToDelete);
                    db.SaveChanges();
                    Investigators.Remove(SelectedInvestigator);
                }
            }
        }

       /* private bool CanAddInvestigator()
        {
            // Check if all required fields have been filled
            return !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(PhoneNumber) &&
                   !string.IsNullOrEmpty(City) &&
                   !string.IsNullOrEmpty(Country) &&
                   PostalCode != 0 &&
                   NumberAdress != 0 &&
                   !string.IsNullOrEmpty(Street);
        }*/

        private String CanAddInvestigator()
        {
            String message = "";

            if (string.IsNullOrEmpty(Name))
            {
                message += "- Le nom de l'inspecteur est vide !\n";
            }
            if (string.IsNullOrEmpty(LastName))
            {
                message += "- Le prénom de l'inspecteur est vide !\n";
            }
            if (string.IsNullOrEmpty(Email))
            {
                message += "- Le mail de l'inspecteur est vide !\n";
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                message += "- Le téléphone de l'inspecteur est vide !\n";
            }
            if (string.IsNullOrEmpty(City))
            {
                message += "- La ville de l'inspecteur est vide !\n";
            }
            if (string.IsNullOrEmpty(Country))
            {
                message += "- Le pays de l'inspecteur est vide !\n";
            }
            if (PostalCode == 0)
            {
                message += "- Le code postale de l'inspecteur est vide !\n";
            }
            if (NumberAdress == 0)
            {
                message += "- Le numéro de la rue de l'inspecteur est vide !\n";
            }
            if (string.IsNullOrEmpty(Street))
            {
                message += "- La rue de l'inspecteur est vide !\n";
            }

            return message;
        }

        private void ClearInvestigatorFields()
        {
            SelectedInvestigator = null;
            Name = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            PostalCode = 0;
            NumberAdress = 0;
            Street = string.Empty;
        }

        private void FillTextBox()
        {
            if (SelectedInvestigator != null)
            {
                Name = SelectedInvestigator.Name;
                LastName = SelectedInvestigator.LastName;
                Country = SelectedInvestigator.Country;
                City = SelectedInvestigator.City;
                Email = SelectedInvestigator.Email;
                PostalCode = SelectedInvestigator.PostalCode;
                NumberAdress = SelectedInvestigator.NumberAdress;
                Street = SelectedInvestigator.Street;
                PhoneNumber = SelectedInvestigator.PhoneNumber;
            }
        }
    }

}
