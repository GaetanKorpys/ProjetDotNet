using CommunityToolkit.Mvvm.Input;
using ProjetDotNet.Database;
using ProjetDotNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ProjetDotNet.ViewModels
{
    public class ComplainantViewModel : BaseViewModel
    {
        private Complainant _selectedComplainant;
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


        public Complainant SelectedComplainant
        {
            get { return _selectedComplainant; }
            set
            {
                _selectedComplainant = value;
                FillTextBox();
                OnPropertyChanged(nameof(SelectedComplainant));
            }
        }

        public ObservableCollection<Complainant> Complainants { get; set; }

        public ICommand AddComplainantCommand { get; set; }
        public ICommand UpdateComplainantCommand { get; set; }
        public ICommand DeleteComplainantCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }



        // Constructor
        public ComplainantViewModel()
        {
            AddComplainantCommand = new RelayCommand(AddComplainant);
            UpdateComplainantCommand = new RelayCommand(UpdateComplainant);
            DeleteComplainantCommand = new RelayCommand(DeleteComplainant);
            ClearFieldsCommand = new RelayCommand(ClearInvestigatorFields);

            using (var context = new ApplicationContext())
            {
                var complainants = context.Complainants.ToList();
                Complainants = new ObservableCollection<Complainant>(complainants);
            }
        }

        private void AddComplainant()
        {
            String erreur = CanAddComplainant();
            if (erreur != "")
            {
                MessageBox.Show("Veuillez remplir tous les champs:\n" + erreur, "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var complainant = new Complainant()
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

                db.Complainants.Add(complainant);
                db.SaveChanges();

                // Add the new investigator to the list of investigators
                Complainants.Add(complainant);

                ClearInvestigatorFields();
            }
        }

        private void UpdateComplainant()
        {
            if (SelectedComplainant == null)
            {
                MessageBox.Show("Veuillez sélectionner un plaignant à modifier.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var complainantToUpdate = db.Complainants.Find(SelectedComplainant.ComplainantId);

                if (complainantToUpdate != null)
                {
                    complainantToUpdate.Name = Name;
                    complainantToUpdate.LastName = LastName;
                    complainantToUpdate.Email = Email;
                    complainantToUpdate.PhoneNumber = PhoneNumber;
                    complainantToUpdate.City = City;
                    complainantToUpdate.Country = Country;
                    complainantToUpdate.PostalCode = PostalCode;
                    complainantToUpdate.NumberAdress = NumberAdress;
                    complainantToUpdate.Street = Street;

                    db.SaveChanges();
                }
                ClearInvestigatorFields();
            }
        }

        private void DeleteComplainant()
        {
            if (SelectedComplainant == null)
            {
                MessageBox.Show("Veuillez sélectionner un plaignant à supprimer.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var complainantToDelete = db.Complainants.Find(SelectedComplainant.ComplainantId);

                if (complainantToDelete != null)
                {
                    db.Complainants.Remove(complainantToDelete);
                    db.SaveChanges();
                    Complainants.Remove(complainantToDelete);
                }
            }
        }

        /*private bool CanAddComplainant()
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

        private String CanAddComplainant()
        {
            String message = "";

            if (string.IsNullOrEmpty(Name))
            {
                message += "- Le nom du plaignant est vide !\n";
            }
            if (string.IsNullOrEmpty(LastName))
            {
                message += "- Le prénom du plaignant est vide !\n";
            }
            if (string.IsNullOrEmpty(Email))
            {
                message += "- Le mail du plaignant est vide !\n";
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                message += "- Le téléphone du plaignant est vide !\n";
            }
            if (string.IsNullOrEmpty(City))
            {
                message += "- La ville du plaignant est vide !\n";
            }
            if (string.IsNullOrEmpty(Country))
            {
                message += "- Le pays du plaignant est vide !\n";
            }
            if (PostalCode == 0)
            {
                message += "- Le code postale du plaignant est vide !\n";
            }
            if (NumberAdress == 0)
            {
                message += "- Le numéro de la rue du plaignant est vide !\n";
            }
            if (string.IsNullOrEmpty(Street))
            {
                message += "- La rue du plaignant est vide !\n";
            }

            return message;
        }


        private void ClearInvestigatorFields()
        {
            SelectedComplainant = null;
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
            if (SelectedComplainant != null)
            {
                Name = SelectedComplainant.Name;
                LastName = SelectedComplainant.LastName;
                Country = SelectedComplainant.Country;
                City = SelectedComplainant.City;
                Email = SelectedComplainant.Email;
                PostalCode = SelectedComplainant.PostalCode;
                NumberAdress = SelectedComplainant.NumberAdress;
                Street = SelectedComplainant.Street;
                PhoneNumber = SelectedComplainant.PhoneNumber;
            }
        }
    }
}
