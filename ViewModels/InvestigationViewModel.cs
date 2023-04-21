using CefSharp;
using CefSharp.Wpf;
using CommunityToolkit.Mvvm.Input;
using ProjetDotNet.Database;
using ProjetDotNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjetDotNet.ViewModels
{
    public class InvestigationViewModel : BaseViewModel
    {
        private ChromiumWebBrowser _webBrowser = new ChromiumWebBrowser();
        private Investigation _selectedInvestigation;
        private Investigator _selectedInvestigator;
        private Suspect _selectedSuspect;
        private Complainant _selectedComplainant;
        private int _numberAnimals;
        private string _reason;
        private string _comments;
        private string _animalType;
        private bool _isMainInvestigator;

        public int NumberAnimals
        {
            get { return _numberAnimals; } 
            set 
            { 
                _numberAnimals = value;
                OnPropertyChanged(nameof(NumberAnimals));
            }
        }

        public string Reason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }

        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public string AnimalType
        {
            get { return _animalType; }
            set
            {
                _animalType = value;
                OnPropertyChanged(nameof(AnimalType));
            }
        }

        public bool IsMainInvestigator
        {
            get { return _isMainInvestigator; }
            set
            {
                _isMainInvestigator = value;
                OnPropertyChanged(nameof(IsMainInvestigator));
            }
        }

        public Investigation SelectedInvestigation
        {
            get { return _selectedInvestigation; }
            set
            {
                _selectedInvestigation = value;
                OnPropertyChanged(nameof(SelectedInvestigation));
            }
        }

        public Investigator SelectedInvestigator
        { 
            get { return _selectedInvestigator;}
            set
            {
                _selectedInvestigator = value;
                OnPropertyChanged(nameof(SelectedInvestigator));
            }
        }

        public Suspect SelectedSuspect
        {
            get { return _selectedSuspect; }
            set
            {
                _selectedSuspect = value;
                OnPropertyChanged(nameof(SelectedSuspect));
            }
        }


        public Complainant SelectedComplainant
        {
            get { return _selectedComplainant; }
            set
            {
                _selectedComplainant = value;
                OnPropertyChanged(nameof(SelectedComplainant));
            }
        }

        public ChromiumWebBrowser WebBrowser 
        {
            get { return _webBrowser; }
            set
            {
                _webBrowser = value;
                OnPropertyChanged(nameof(WebBrowser));
            }
        }

        public ObservableCollection<Investigation> Investigations { get; set; }
        public ObservableCollection<Investigator> Investigators { get; set; }
        public ObservableCollection<Suspect> Suspects { get; set; }
        public ObservableCollection<Complainant> Complainants { get; set; }

        public ICommand AddInvestigationCommand { get; set; }
        public ICommand UpdateInvestigationCommand { get; set; }
        public ICommand DeleteInvestigationCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }
        public ICommand LoadHtmlCommand { get; set; }

        // Constructor
        public InvestigationViewModel()
        {
            AddInvestigationCommand = new RelayCommand(AddInvestigator);
            UpdateInvestigationCommand = new RelayCommand(UpdateInvestigator);
            DeleteInvestigationCommand = new RelayCommand(DeleteInvestigator);
            ClearFieldsCommand = new RelayCommand(ClearInvestigatorFields);
            LoadHtmlCommand = new RelayCommand(UpdateWebBrowser);

            using (var context = new ApplicationContext())
            {
                var investigators = context.Investigators.ToList();
                Investigators = new ObservableCollection<Investigator>(investigators);

                var suspects = context.Suspects.ToList();
                Suspects = new ObservableCollection<Suspect>(suspects);

                var complainants = context.Complainants.ToList();
                Complainants = new ObservableCollection<Complainant>(complainants);
            }           

            //Pour WebBrowser
            /*
             * TODO
            var settings = new CefSharp.WinForms.CefSettings();
            settings.CefCommandLineArgs.Add("disable-web-security");
            _webBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=Nancy&destination=Metz&avoid=tolls\" width=\"800\" height=\"400\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
            MyStackPanel.Children.Add(_webBrowser);
            */
        }

        private void ClearInvestigatorFields()
        {
            SelectedInvestigator = null;
            SelectedComplainant = null;
            SelectedSuspect = null;
            Comments = string.Empty;
            Reason = string.Empty;
            AnimalType = string.Empty;
            NumberAnimals = 0; 
           
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

        private void UpdateInvestigator()
        {
            throw new NotImplementedException();
        }

        private void AddInvestigator()
        {
            throw new NotImplementedException();
        }

        private void UpdateWebBrowser()
        {
            Console.WriteLine("okkk");
            _webBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=Nancy&destination=Metz&avoid=tolls\" width=\"800\" height=\"400\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
        }
    }
}
