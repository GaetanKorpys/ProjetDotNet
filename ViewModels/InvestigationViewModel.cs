using CefSharp;
using CefSharp.Wpf;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using ProjetDotNet.Database;
using ProjetDotNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace ProjetDotNet.ViewModels
{
    public class InvestigationViewModel : BaseViewModel
    {
        private string _webBrowserAddress;
        private ChromiumWebBrowser _webBrowser;
        private Investigation _selectedInvestigation;
        private Investigator _selectedInvestigator;
        private Suspect _selectedSuspect;
        private Complainant _selectedComplainant;
        private int _numberAnimals;
        private string _reason;
        private string _comments;
        private string _animalType;
        private int _numberInvestigationInCharge;
        private int _numberInvestigationNotInCharge;
        private Status _status;
        private bool _readOnly;
        private bool _enabledAddButton;
        private bool _enabledUpdateButton;
        private bool _enabledFinishButton;
        private bool _enabledFileComplaintButton;
        private Visibility _enabledStatus;

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

        public int NumberInvestigationInCharge 
        {
            get { return _numberInvestigationInCharge; }
            set
            {
                _numberInvestigationInCharge = value;
                OnPropertyChanged(nameof(NumberInvestigationInCharge));
            }
        }

        public int NumberInvestigationNotInCharge
        {
            get { return _numberInvestigationNotInCharge; }
            set
            {
                _numberInvestigationNotInCharge = value;
                OnPropertyChanged(nameof(NumberInvestigationNotInCharge));
            }
        }

        public Status Status 
        {
            get { return _status; }
            set
            {
               _status  = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        public bool EnabledAddButton
        {
            get { return _enabledAddButton; }
            set
            {
                _enabledAddButton = value;
                OnPropertyChanged(nameof(EnabledAddButton));
            }
        }

        public bool EnabledUpdateButton
        {
            get { return _enabledUpdateButton; }
            set
            {
                _enabledUpdateButton = value;
                OnPropertyChanged(nameof(EnabledUpdateButton));
            }
        }

        public bool EnabledFinishButton
        {
            get { return _enabledFinishButton; }
            set
            {
                _enabledFinishButton = value;
                OnPropertyChanged(nameof(EnabledFinishButton));
            }
        }

        public bool EnabledFileComplaintButton
        {
            get { return _enabledFileComplaintButton; }
            set
            {
                _enabledFileComplaintButton = value;
                OnPropertyChanged(nameof(EnabledFileComplaintButton));
            }
        }

        public Visibility EnabledStatus
        {
            get { return _enabledStatus; }
            set
            {
                _enabledStatus = value;
                OnPropertyChanged(nameof(EnabledStatus));
            }
        }


        public Investigation SelectedInvestigation
        {
            get { return _selectedInvestigation; }
            set
            {
                _selectedInvestigation = value;
                OnPropertyChanged(nameof(SelectedInvestigation));
                FillTextBox();


                if (_selectedInvestigation != null)
                {
                    if (_selectedInvestigation.Status == Status.Classée || _selectedInvestigation.Status == Status.Dépot_Plainte)
                    {
                        ReadOnly = true;
                        EnabledUpdateButton = false;
                        EnabledFinishButton = false;
                        EnabledFileComplaintButton = false;
                    }

                    else
                    {
                        ReadOnly = false;
                        EnabledUpdateButton = true;
                        EnabledFinishButton = true;
                        EnabledFileComplaintButton = true;
                    }

                    EnabledAddButton = false;
                    EnabledStatus = Visibility.Visible;

                }
                else
                {
                    ReadOnly = false;
                    EnabledAddButton = true;
                    EnabledStatus = Visibility.Hidden;
                    EnabledUpdateButton = false;
                    EnabledFinishButton = false;
                    EnabledFileComplaintButton = false;
                }

            }
        }

        private void FillTextBox()
        {
            if (SelectedInvestigation != null)
            {
                Reason = SelectedInvestigation.Reason;
                Comments = SelectedInvestigation.Comments;
                AnimalType = SelectedInvestigation.AnimalBreed;
                NumberAnimals = SelectedInvestigation.NumberOfAnimals;
                SelectedInvestigator = SelectedInvestigation.Investigator;
                SelectedSuspect = SelectedInvestigation.Suspect;
                SelectedComplainant = SelectedInvestigation.Complainant;
                Status = SelectedInvestigation.Status;
            }
        }

        public Investigator SelectedInvestigator
        { 
            get { return _selectedInvestigator;}
            set
            {
                _selectedInvestigator = value;
                OnPropertyChanged(nameof(SelectedInvestigator));

                using (var context = new ApplicationContext())
                {

                    NumberInvestigationInCharge = context.Investigations.Where(i =>i.Status == Status.En_cours && i.Investigator == SelectedInvestigator).Count();
                    var tmp = context.Visits.Where(v => v.Investigators.Contains(SelectedInvestigator)).Count();
                    if (tmp < 0)
                        NumberInvestigationNotInCharge = 0;
                    else
                        NumberInvestigationNotInCharge = tmp;

                }


                if (SelectedSuspect != null && _selectedInvestigator != null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=" + _selectedInvestigator.City + " " + _selectedInvestigator.Street + "&destination=" + SelectedSuspect.City + " " + SelectedSuspect.Street + "&avoid=tolls\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
                else if (SelectedSuspect != null && _selectedInvestigator == null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=" + SelectedSuspect.City + " " + SelectedSuspect.Street + "&destination=" + SelectedSuspect.City + " " + SelectedSuspect.Street + "&avoid=tolls\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
                else if (SelectedSuspect == null && _selectedInvestigator == null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/place?q=France&key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
                else if (SelectedSuspect == null && _selectedInvestigator != null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=" + _selectedInvestigator.City + " " + _selectedInvestigator.Street + "&destination=" + _selectedInvestigator.City + " " + _selectedInvestigator.Street + "&avoid=tolls\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
            }
        }

        public Suspect SelectedSuspect
        {
            get { return _selectedSuspect; }
            set
            {
                _selectedSuspect = value;
                OnPropertyChanged(nameof(SelectedSuspect));
                if (_selectedSuspect != null && SelectedInvestigator != null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=" + SelectedInvestigator.City + " " + SelectedInvestigator.Street + "&destination=" + _selectedSuspect.City + " " + _selectedSuspect.Street + "&avoid=tolls\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
                else if (_selectedSuspect != null && SelectedInvestigator == null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=" + _selectedSuspect.City + " " + _selectedSuspect.Street +"&destination=" + _selectedSuspect.City + " " + _selectedSuspect.Street + "&avoid=tolls\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
                else if (_selectedSuspect == null && SelectedInvestigator == null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/place?q=France&key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
                else if (_selectedSuspect == null && SelectedInvestigator != null)
                    WebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=" + SelectedInvestigator.City +" "+ SelectedInvestigator.Street + "&destination=" + SelectedInvestigator.City + " " + SelectedInvestigator.Street + "&avoid=tolls\" width=\"100%\" height=\"100%\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
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

        public string WebBrowserAddress
        {
            get { return _webBrowserAddress; }
            set
            {
                _webBrowserAddress = value;
                OnPropertyChanged(nameof(WebBrowser));
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
        public ICommand FinishInvestigationCommand { get; set; }
        public ICommand FileComplaintCommand { get; set; }
        

        // Constructor
        public InvestigationViewModel()
        {
            AddInvestigationCommand = new RelayCommand(AddInvestigation);
            UpdateInvestigationCommand = new RelayCommand(UpdateInvestigation);
            DeleteInvestigationCommand = new RelayCommand(DeleteInvestigation);
            ClearFieldsCommand = new RelayCommand(ClearInvestigationFields);
            FinishInvestigationCommand = new RelayCommand(FinishInvestigation);
            FileComplaintCommand = new RelayCommand(FileComplaint);


            //Config button at launch
            ReadOnly = false;
            EnabledAddButton = true;
            EnabledStatus = Visibility.Hidden;
            EnabledUpdateButton = false;
            EnabledFinishButton = false;
            EnabledFileComplaintButton = false;

            var settings = new CefSharp.WinForms.CefSettings();
            settings.CefCommandLineArgs.Add("disable-web-security");

            using (var context = new ApplicationContext())
            {
                var investigations = context.Investigations.ToList();
                Investigations = new ObservableCollection<Investigation>(investigations);

                var investigators = context.Investigators.ToList();
                Investigators = new ObservableCollection<Investigator>(investigators);

                var suspects = context.Suspects.ToList();
                Suspects = new ObservableCollection<Suspect>(suspects);

                var complainants = context.Complainants.ToList();
                Complainants = new ObservableCollection<Complainant>(complainants);
            }          
        }

        private void ClearInvestigationFields()
        {
            SelectedInvestigation = null;
            SelectedInvestigator = null;
            SelectedComplainant = null;
            SelectedSuspect = null;
            Comments = string.Empty;
            Reason = string.Empty;
            AnimalType = string.Empty;
            NumberAnimals = 0;
        }

        private void DeleteInvestigation()
        {
            if (SelectedInvestigation == null)
            {
                MessageBox.Show("Veuillez sélectionner une enquete à supprimer.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigationToDelete = db.Investigations.Find(SelectedInvestigation.InvestigationId);

                if (investigationToDelete != null)
                {

                    var suspects = db.Suspects.Where(s => s.Investigation == investigationToDelete).ToList();
                    foreach (var suspect in suspects)
                    {
                        db.Suspects.Remove(suspect);
                    }

                    var complainants = db.Complainants.Where(c => c.Investigation == investigationToDelete).ToList();
                    foreach (var complainant in complainants)
                    {
                        db.Complainants.Remove(complainant);
                    }

                    var visits = db.Visits.Where(c => c.Investigation == investigationToDelete).ToList();
                    foreach (var visit in visits)
                    {
                        db.Visits.Remove(visit);
                    }

                    db.Investigations.Remove(investigationToDelete);
                    db.SaveChanges();
                    Investigations.Remove(SelectedInvestigation);
                }
            }

            ClearInvestigationFields();
        }

        private void UpdateInvestigation()
        {
            if (SelectedInvestigation == null)
            {
                MessageBox.Show("Veuillez sélectionner une unquête à modifier.", "Erreur");
                return;
            }

            if (!CanAddInvestigation())
            {
                MessageBox.Show("Veuillez remplir les champs nécéssaires.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigationToUpdate = db.Investigations.Find(SelectedInvestigation.InvestigationId);
                var investigator = db.Investigators.Find(SelectedInvestigator.InvestigatorId);
                var suspect = db.Suspects.Find(SelectedSuspect.SuspectId);
                var complainant = db.Complainants.Find(SelectedComplainant.ComplainantId);


                if (investigationToUpdate != null)
                {
                    investigationToUpdate.Comments = Comments;
                    investigationToUpdate.Reason = Reason;
                    investigationToUpdate.AnimalBreed = AnimalType;
                    investigationToUpdate.NumberOfAnimals = NumberAnimals;
                    investigationToUpdate.Investigator = investigator;
                    //investigationToUpdate.Suspect = suspect;
                    //investigationToUpdate.Complainant = complainant;

                    db.SaveChanges();
                }
                ClearInvestigationFields();

            }


        }

        private void AddInvestigation()
        {
            if (!CanAddInvestigation())
            {
                MessageBox.Show("Veuillez remplir les champs nécéssaires.", "Erreur");
                return; 
            }

            if (SelectedInvestigator != null)
            {
                using (var db = new ApplicationContext())
                {
                    var investigator = db.Investigators.Find(SelectedInvestigator.InvestigatorId);
                    var suspect = db.Suspects.Find(SelectedSuspect.SuspectId);
                    var complainant = db.Complainants.Find(SelectedComplainant.ComplainantId);

                    var investigation = new Investigation()
                    {
                        Reason = Reason,
                        NumberOfAnimals = NumberAnimals,
                        AnimalBreed = AnimalType,
                        Comments = Comments,
                        InvestigationStartDate = DateTime.Now.Date,
                        Suspect = suspect,
                        Complainant = complainant,
                        Investigator = investigator,
                        Status = Status.En_cours

                    };

                    investigator.Investigations.Add(investigation);

                    db.Investigations.Add(investigation);
                    db.SaveChanges();

                    // Add the new investigator to the list of investigators
                    Investigations.Add(investigation);
                }

            }
            else
            {
                using (var db = new ApplicationContext())
                {
                    var investigation = new Investigation()
                    {
                        Reason = Reason,
                        NumberOfAnimals = NumberAnimals,
                        AnimalBreed = AnimalType,
                        Comments = Comments,
                        InvestigationStartDate = DateTime.Now.Date,
                        Suspect = SelectedSuspect,
                        Complainant = SelectedComplainant,
                        Status = Status.En_attente

                    };
                    db.Investigations.Add(investigation);
                    db.SaveChanges();

                    // Add the new investigator to the list of investigators
                    Investigations.Add(investigation);
                }
            }
            ClearInvestigationFields();
        }

        private bool CanAddInvestigation()
        {
            return !string.IsNullOrEmpty(Reason) &&
                  NumberAnimals != 0 &&
                  !string.IsNullOrEmpty(AnimalType) &&
                  SelectedSuspect != null &&
                  SelectedComplainant != null &&
                  SelectedInvestigator != null;
        }

        private void FinishInvestigation()
        {
            if (SelectedInvestigation == null)
            {
                MessageBox.Show("Veuillez sélectionner une enquête.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigationToFinish = db.Investigations.Find(SelectedInvestigation.InvestigationId);

                if (investigationToFinish != null)
                {
                    investigationToFinish.Status = Status.Classée;
                    investigationToFinish.InvestigationEndDate = DateTime.Now.Date;
                    db.SaveChanges();
                }
                ClearInvestigationFields();
            }
        }

        private void FileComplaint()
        {
            if (SelectedInvestigation == null)
            {
                MessageBox.Show("Veuillez sélectionner une enquête avant de déposer une plainte.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var investigationToFinish = db.Investigations.Find(SelectedInvestigation.InvestigationId);

                if (investigationToFinish != null)
                {
                    investigationToFinish.Status = Status.Dépot_Plainte;
                    investigationToFinish.InvestigationEndDate = DateTime.Now.Date;
                    db.SaveChanges();
                }
                ClearInvestigationFields();

            }
        }
    }
}
