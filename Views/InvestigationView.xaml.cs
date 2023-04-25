using CefSharp;
using CefSharp.Wpf;
using ProjetDotNet.ViewModels;
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

namespace ProjetDotNet.Views
{
    /// <summary>
    /// Logique d'interaction pour InvestigationView.xaml
    /// </summary>
    public partial class InvestigationView : UserControl
    {
        public InvestigationView()
        {
            InitializeComponent();


            // A mettre dans InvestigationViewModel par la suite
            var settings = new CefSharp.WinForms.CefSettings();
            settings.CefCommandLineArgs.Add("disable-web-security");
            chromWebBrowser.LoadHtml("<html><body><iframe src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyBN4_F3cBbadQ4x1PqZf6_OCktum1dmkJg&origin=Nancy&destination=Metz&avoid=tolls\" width=\"800\" height=\"400\" frameborder=\"0\" style=\"border:0\"></iframe></body></html>");
           
        }

        private void browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.DataContext != null)
            {
                var vm = (InvestigationViewModel)this.DataContext;
                vm.WebBrowser = chromWebBrowser;
            }                 
        }
    }
}
