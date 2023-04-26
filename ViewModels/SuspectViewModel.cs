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
    public class SuspectViewModel : BaseViewModel
    {
        private Suspect _selectedSuspect;
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


        public Suspect SelectedSuspect
        {
            get { return _selectedSuspect; }
            set
            {
                _selectedSuspect = value;
                FillTextBox();
                OnPropertyChanged(nameof(SelectedSuspect));
            }
        }

        public ObservableCollection<Suspect> Suspects { get; set; }

        public ICommand AddSuspectCommand { get; set; }
        public ICommand UpdateSuspectCommand { get; set; }
        public ICommand DeleteSuspectCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }



        // Constructor
        public SuspectViewModel()
        {
            AddSuspectCommand = new RelayCommand(AddSuspect);
            UpdateSuspectCommand = new RelayCommand(UpdateSuspect);
            DeleteSuspectCommand = new RelayCommand(DeleteSuspect);
            ClearFieldsCommand = new RelayCommand(ClearInvestigatorFields);

            using (var context = new ApplicationContext())
            {
                var suspects = context.Suspects.ToList();
                Suspects = new ObservableCollection<Suspect>(suspects);
            }
        }

        private void AddSuspect()
        {

            String erreur = CanAddSuspect();
            if (erreur != "")
            {
                MessageBox.Show("Veuillez remplir les champs nécéssaires:\n" + erreur, "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var suspect = new Suspect()
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

                db.Suspects.Add(suspect);
                db.SaveChanges();

                // Add the new investigator to the list of investigators
                Suspects.Add(suspect);

                ClearInvestigatorFields();
            }
        }

        private void UpdateSuspect()
        {
            if (SelectedSuspect == null)
            {
                MessageBox.Show("Veuillez sélectionner un suspect à modifier.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var suspectToUpdate = db.Suspects.Find(SelectedSuspect.SuspectId);

                if (suspectToUpdate != null)
                {
                    suspectToUpdate.Name = Name;
                    suspectToUpdate.LastName = LastName;
                    suspectToUpdate.Email = Email;
                    suspectToUpdate.PhoneNumber = PhoneNumber;
                    suspectToUpdate.City = City;
                    suspectToUpdate.Country = Country;
                    suspectToUpdate.PostalCode = PostalCode;
                    suspectToUpdate.NumberAdress = NumberAdress;
                    suspectToUpdate.Street = Street;

                    db.SaveChanges();
                }
                ClearInvestigatorFields();
            }
        }

        private void DeleteSuspect()
        {
            if (SelectedSuspect == null)
            {
                MessageBox.Show("Veuillez sélectionner un suspect à supprimer.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var suspectToDelete = db.Suspects.Find(SelectedSuspect.SuspectId);

                if (suspectToDelete != null)
                {
                    db.Suspects.Remove(suspectToDelete);
                    db.SaveChanges();
                    Suspects.Remove(SelectedSuspect);
                }
            }
        }

        /*private bool CanAddSuspect()
        {
            // Check if all required fields have been filled
            return !string.IsNullOrEmpty(City) &&
                   !string.IsNullOrEmpty(Country) &&
                   PostalCode != 0 &&
                   !string.IsNullOrEmpty(Street);
        }*/


        private String CanAddSuspect()
        {
            String message = "";

            if (string.IsNullOrEmpty(City))
            {
                message += "- Le ville du suspect est vide !\n";
            }
            if (string.IsNullOrEmpty(Country))
            {
                message += "- Le pays du suspect est vide !\n";
            }
            if (PostalCode == 0)
            {
                message += "- Le code postal du suspect est vide !\n";
            }
            if (string.IsNullOrEmpty(Street))
            {
                message += "- Le rue du suspect est vide !\n";
            }

            return message;
        }

        private void ClearInvestigatorFields()
        {
            SelectedSuspect = null;
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
            if (SelectedSuspect != null)
            {
                Name = SelectedSuspect.Name;
                LastName = SelectedSuspect.LastName;
                Country = SelectedSuspect.Country;
                City = SelectedSuspect.City;
                Email = SelectedSuspect.Email;
                PostalCode = SelectedSuspect.PostalCode;
                NumberAdress = (int)SelectedSuspect.NumberAdress;
                Street = SelectedSuspect.Street;
                PhoneNumber = SelectedSuspect.PhoneNumber;
            }
        }
    }
}
