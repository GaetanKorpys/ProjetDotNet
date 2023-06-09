﻿using CommunityToolkit.Mvvm.Input;
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
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace ProjetDotNet.ViewModels
{
    public class VisitViewModel : BaseViewModel
    {
        private Visit _selectedVisit;
        private Investigation _selectedInvestigation;
        private Investigator _selectedInvestigator;
        private string _comments;
        private bool _deliveryNotice;
        private bool _isMainInvestigator;
        private ProofPicture _proofPicture;

        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public bool DeliveryNotice 
        {
            get { return _deliveryNotice; }
            set
            {
                _deliveryNotice = value;
                OnPropertyChanged(nameof(DeliveryNotice));
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

        public Visit SelectedVisit
        {
            get { return _selectedVisit; }
            set
            {
                _selectedVisit = value;
                OnPropertyChanged(nameof(SelectedVisit));
                FillTextBox();
            }
        }

        public Investigation SelectedInvestigation
        {
            get { return _selectedInvestigation; }
            set
            {
                _selectedInvestigation = value;

                using (var context = new ApplicationContext())
                {
                    Visits.Clear();
                    
                    var visits = context.Visits.Where(v => v.Investigation == _selectedInvestigation).ToList();
                    foreach (Visit v in visits)
                    {
                        Visits.Add(v);
                    }
                }

                OnPropertyChanged(nameof(SelectedInvestigation));
            }
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

        public ProofPicture SelectProofPicture
        {
            get { return _proofPicture; }
            set
            {
                _proofPicture = value;
                OnPropertyChanged(nameof(SelectProofPicture));
            }
        }

        public ObservableCollection<Image> Images { get; set; }

        private List<ProofPicture> _pictures { get; set; }

        public ObservableCollection<Visit> Visits { get; set; }
        public ObservableCollection<Investigation> Investigations { get; set; }
        public ObservableCollection<Investigator> Investigators { get; set; }

        public ICommand AddProofPictureCommand { get; set; }
        public ICommand AddVisitCommand { get; set; }
        public ICommand UpdateVisitCommand { get; set; }
        public ICommand DeleteVisitCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }

        // Constructor
        public VisitViewModel()
        {
            AddProofPictureCommand = new RelayCommand(AddProofPicture);
            AddVisitCommand = new RelayCommand(AddVisit);
            UpdateVisitCommand = new RelayCommand(UpdateVisit);
            //DeleteVisitCommand = new RelayCommand(DeleteVisit);
            ClearFieldsCommand = new RelayCommand(ClearVisitFields);

            var settings = new CefSharp.WinForms.CefSettings();
            settings.CefCommandLineArgs.Add("disable-web-security");



            Images = new ObservableCollection<Image>();
            Visits = new ObservableCollection<Visit>();
            _pictures = new List<ProofPicture>();

            using (var context = new ApplicationContext())
            {
                
                var investigations = context.Investigations.ToList();
                Investigations = new ObservableCollection<Investigation>(investigations);

                var investigators = context.Investigators.ToList();
                Investigators = new ObservableCollection<Investigator>(investigators);

            }
        }

        private async void AddProofPicture()
        {
            Console.WriteLine("HELOO");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files(*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;.bmp;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    // Charger l'image depuis le fichier sélectionné
                    var pictureBytes = await File.ReadAllBytesAsync(filename);

                    BitmapImage image = new BitmapImage(new Uri(filename));
                    MemoryStream ms = new MemoryStream();
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(ms);
                    byte[] buffer = ms.ToArray();
                    string base64String = Convert.ToBase64String(buffer);
                    Image imageControl = new Image();
                    imageControl.Width = 200;
                    imageControl.Height = 200;
                    imageControl.Margin = new Thickness(5);
                    imageControl.Source = image;
                    imageControl.Tag = base64String;
                    //ProofPicture newPicture = new ProofPicture { Picture = buffer };


                    Images.Add(imageControl);

                    using (var db = new ApplicationContext())
                    {
                        var picture = new ProofPicture()
                        {
                            Picture = pictureBytes
                        };

                        db.ProofPictures.Add(picture);
                        await db.SaveChangesAsync().ConfigureAwait(false);

                        _pictures.Add(picture);

                    }
                }
            }
        }


        private void ClearVisitFields()
        {
            SelectedVisit = null;
            SelectedInvestigation = null;
            SelectedInvestigator = null;
            Comments = string.Empty;
            DeliveryNotice = false;
            Images.Clear();
            _pictures.Clear();
        }

        private void DeleteVisit()
        {
            if (SelectedInvestigation == null)
            {
                MessageBox.Show("Veuillez sélectionner une enquête puis la visite à modifier.", "Erreur");
                return;
            }

            if (SelectedVisit == null)
            {
                MessageBox.Show("Veuillez sélectionner une visite à modifier.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var visitToDelete = db.Visits.Find(SelectedVisit.VisitId);

                if (visitToDelete != null)
                {
                    
                    var proofPictures = db.ProofPictures.Where(c => c.Visit == visitToDelete).ToList();
                    foreach (var p in proofPictures)
                    {
                        db.ProofPictures.Remove(p);
                    }

                    db.Visits.Remove(visitToDelete);
                    db.SaveChanges();
                    Visits.Remove(SelectedVisit);
                }
            }
            ClearVisitFields();
        }

        private void UpdateVisit()
        {

            if(SelectedInvestigation == null)
            {
                MessageBox.Show("Veuillez sélectionner une enquête puis la visite à modifier.", "Erreur");
                return;
            }

            if (SelectedVisit == null)
            {
                MessageBox.Show("Veuillez sélectionner une visite à modifier.", "Erreur");
                return;
            }

            using (var db = new ApplicationContext())
            {
                var visitToUpdate = db.Visits.Find(SelectedVisit.VisitId);

                if (visitToUpdate != null)
                {
                    visitToUpdate.Comments = Comments;
                    visitToUpdate.DeliveryNotice = DeliveryNotice;
                    visitToUpdate.ProofPictures = _pictures;
                    db.SaveChanges();
                }
                ClearVisitFields();
            }
        }

        private void AddVisit()
        {

            String erreur = CanAddVisit();
            if (erreur != "")
            {
                MessageBox.Show("Veuillez remplir les champs nécéssaires:\n" + erreur, "Erreur");
                return;
            }

            if (SelectedVisit != null)
            {
                MessageBox.Show("Une visite est déjà séléctionnée.\n" +"Vous pouvez cependant la mettre à jour via le bouton Update.");
                return;
            }

            if (SelectedInvestigator != null && SelectedInvestigation != null)
            {
                using (var db = new ApplicationContext())
                {
                    var investigator = db.Investigators.Find(SelectedInvestigator.InvestigatorId);
                    var investigation = db.Investigations.Find(SelectedInvestigation.InvestigationId);

                    var visit = new Visit()
                    {
                        Comments = Comments,
                        DeliveryNotice = DeliveryNotice,
                        VisitDate = DateTime.Now.Date,
                        Investigation = investigation
                    };

                    visit.Investigators.Add(investigator);

                    foreach(ProofPicture p in _pictures)
                    {
                        var picture = db.ProofPictures.Find(p.ProofPictureId);
                        /*
                        var newPicture = new ProofPicture()
                        {
                            Picture = p.Picture,
                        };
                        */
                        visit.ProofPictures.Add(picture);
                    }

                    db.Visits.Add(visit);
                    db.SaveChanges();

                    investigation.Visits.Add(visit);
                }

                _pictures.Clear();
                Images.Clear();

            }           
            ClearVisitFields();
        }

        private String CanAddVisit()
        {
            String message = "";

            if (SelectedInvestigation == null)
            {
                message += "- L'enquête n'est pas sélectionné !\n";
            }
            if (SelectedInvestigator == null)
            {
                message += "- L'enquêteur n'est pas sélectionné !\n";
            }
            if (string.IsNullOrEmpty(Comments))
            {
                message += "- Le commentaire est vide !\n";
            }

            return message;

        }

        private string ConvertImageToBase64(BitmapImage image)
        {
            MemoryStream ms = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(ms);
            byte[] buffer = ms.ToArray();
            return Convert.ToBase64String(buffer);
        }

        private void FillTextBox()
        {

            Images.Clear();
            _pictures.Clear();

            if (SelectedVisit != null)
            {
                Comments = SelectedVisit.Comments;
                DeliveryNotice = SelectedVisit.DeliveryNotice;



                using (var db = new ApplicationContext())
                {
                    var pictures = db.ProofPictures.Where(p => p.VisitId == SelectedVisit.VisitId).ToList();
                    foreach (var p in pictures)
                    {
                        var imageSource = ConvertArrayByteToImage(p.Picture);
                        var image = new Image
                        {
                            Source = imageSource,
                            Width = 100,
                            Height = 100
                        };
                        Images.Add(image);
                        _pictures.Add(p);
                    }
                }
            }
        }



        private ImageSource ConvertArrayByteToImage(byte[] picture)
        {
            if (picture == null || picture.Length == 0)
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(picture))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }


    }
}
