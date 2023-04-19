using CommunityToolkit.Mvvm.Input;
using ProjetDotNet.Database;
using ProjetDotNet.Model;
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

namespace ProjetDotNet
{
    public class InvestigatorViewModel : INotifyPropertyChanged
    {

        private Investigator _selectedInvestigator;
        private bool _isMain;
        private string _name;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _city;
        private string _country;
        private int _postalCode;
        private int _numberAdress;
        private string _street;

        public bool IsMain
        {
            get { return _isMain; }
            set
            {
                _isMain = value;
                OnPropertyChanged(nameof(IsMain));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));
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

        public string FullName
        {
            get { return $"{Name} {LastName}"; }
        }

        public Investigator SelectedInvestigator
        {
            get { return _selectedInvestigator; }
            set
            {
                _selectedInvestigator = value;
                OnPropertyChanged(nameof(SelectedInvestigator));
            }
        }

        public ObservableCollection<Investigator> Investigators { get; set; }

        public ICommand AddInvestigatorCommand { get; set; }
        public ICommand UpdateInvestigatorCommand { get; set; }
        public ICommand DeleteInvestigatorCommand { get; set; }

        // Implémentation de INotifyPropertyChanged pour la liaison de données
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor
        public InvestigatorViewModel()
        {
            AddInvestigatorCommand = new RelayCommand(AddInvestigator);
            UpdateInvestigatorCommand = new RelayCommand(UpdateInvestigator);
            DeleteInvestigatorCommand = new RelayCommand(DeleteInvestigator);

            using (var context = new ApplicationContext())
            {
                var investigators = context.Investigators.ToList();
                Investigators = new ObservableCollection<Investigator>(investigators);
            }
        }

        
        //CRUD
        private void AddInvestigator()
        {
            if(!CanAddInvestigator())
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigator = new Investigator()
                {
                    IsMain = this.IsMain,
                    Name = this.Name,
                    LastName = this.LastName,
                    Email = this.Email,
                    PhoneNumber = this.PhoneNumber,
                    City = this.City,
                    Country = this.Country,
                    PostalCode = this.PostalCode,
                    NumberAdress = this.NumberAdress,
                    Street = this.Street
                };

                db.Investigators.Add(investigator);
                db.SaveChanges();

                // Add the new investigator to the list of investigators
                this.Investigators.Add(investigator);

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
                    investigatorToUpdate.IsMain = IsMain;
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
                using (var context = new ApplicationContext())
                {
                    var investigators = context.Investigators.ToList();
                    Investigators = new ObservableCollection<Investigator>(investigators);
                }
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

        private bool CanAddInvestigator()
        {
            // Check if all required fields have been filled
            return !string.IsNullOrEmpty(this.Name) &&
                   !string.IsNullOrEmpty(this.LastName) &&
                   !string.IsNullOrEmpty(this.Email) &&
                   !string.IsNullOrEmpty(this.PhoneNumber) &&
                   !string.IsNullOrEmpty(this.City) &&
                   !string.IsNullOrEmpty(this.Country) &&
                   this.PostalCode != 0 &&
                   this.NumberAdress != 0 &&
                   !string.IsNullOrEmpty(this.Street);
        }

        public void ClearInvestigatorFields()
        {
            SelectedInvestigator = null;
            IsMain = false;
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

        public void FillTextBox()
        {
            if(SelectedInvestigator != null) 
            {
                IsMain = SelectedInvestigator.IsMain;
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
