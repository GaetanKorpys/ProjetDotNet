using CefSharp.Wpf;
using CommunityToolkit.Mvvm.Input;
using ProjetDotNet.Database;
using ProjetDotNet.Models;
using ProjetDotNet.Views;
using ProjetDotNet.ViewModels;
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
        /*
        private readonly InvestigationViewModel _investigationViewModel;
        private readonly InvestigatorViewModel _investigatorViewModel;
        private readonly SuspectViewModel _suspectViewModel;
        private readonly ComplainantViewModel _complainantViewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            _investigationViewModel = new InvestigationViewModel();
            _investigatorViewModel = new InvestigatorViewModel();
            _suspectViewModel = new SuspectViewModel();
            _complainantViewModel = new ComplainantViewModel();
            DataContext = this;
        }
        */
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void InvestigationView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new InvestigationViewModel();
        }

        private void InvestigatorView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new InvestigatorViewModel();
        }

        private void SuspectView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SuspectViewModel();
        }

        private void ComplainantView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ComplainantViewModel();
        }


        /*
        public RelayCommand OpenInvestigationCommand => new RelayCommand(OpenInvestigation);
        public RelayCommand OpenInvestigatorCommand => new RelayCommand(OpenInvestigator);
        public RelayCommand OpenSuspectCommand => new RelayCommand(OpenSuspect);
        public RelayCommand OpenComplainantCommand => new RelayCommand(OpenComplainant);

        private void OpenInvestigation()
        {
            // Créer une instance de la vue d'Investigation
            var investigationView = new InvestigationView();

            // Assigner le ViewModel à la vue
            investigationView.DataContext = _investigationViewModel;

            // Ajouter la vue en tant que nouveau TabItem dans le TabControl de MainWindow
            var newTabItem = new TabItem()
            {
                Header = "Investigation",
                Content = investigationView
            };
            tabControl.Items.Add(newTabItem);

            // Sélectionner le nouveau TabItem
            tabControl.SelectedItem = newTabItem;
        }

        private void OpenInvestigator()
        {
            // Créer une instance de la vue d'Investigator
            var investigatorView = new InvestigatorView();

            // Assigner le ViewModel à la vue
            investigatorView.DataContext = _investigatorViewModel;

            // Ajouter la vue en tant que nouveau TabItem dans le TabControl de MainWindow
            var newTabItem = new TabItem()
            {
                Header = "Investigator",
                Content = investigatorView
            };
            tabControl.Items.Add(newTabItem);

            // Sélectionner le nouveau TabItem
            tabControl.SelectedItem = newTabItem;
        }

        private void OpenSuspect()
        {
            // Créer une instance de la vue de Suspect
            var suspectView = new SuspectView();

            // Assigner le ViewModel à la vue
            suspectView.DataContext = _suspectViewModel;

            // Ajouter la vue en tant que nouveau TabItem dans le TabControl de MainWindow
            var newTabItem = new TabItem()
            {
                Header = "Suspect",
                Content = suspectView
            };
            tabControl.Items.Add(newTabItem);

            // Sélectionner le nouveau TabItem
            tabControl.SelectedItem = newTabItem;
        }

        private void OpenComplainant()
        {
            // Créer une instance de la vue de Complainant
            var complainantView = new ComplainantView();

            // Assigner le ViewModel à la vue
            complainantView.DataContext = _complainantViewModel;

            // Ajouter la vue en tant que nouveau TabItem dans le TabControl de MainWindow
            var newTabItem = new TabItem()
            {
                Header = "Complainant",
                Content = complainantView
            };
            tabControl.Items.Add(newTabItem);

            // Sélectionner le nouveau TabItem
            tabControl.SelectedItem = newTabItem;
        }
        */
    }
}
