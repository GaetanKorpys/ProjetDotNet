using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetDotNet.Views
{
    /// <summary>
    /// Logique d'interaction pour VisitView.xaml
    /// </summary>
    public partial class VisitView : UserControl
    {
        public VisitView()
        {
            InitializeComponent();
        }

        private void AddInvestigatorGrid(object sender, RoutedEventArgs e)
        {
            StackPanel myStackPanel = (StackPanel)this.FindName("spInvestigator");
            string gridString = CreateGridAddInvestigator();

            // Créer un objet Grid à partir de la chaîne de caractères
            Grid grid = (Grid)XamlReader.Parse(gridString);

            // Ajouter le Grid au StackPanel
            myStackPanel.Children.Add(grid);
        }

        public static string CreateGridAddInvestigator()
        {
            string grid = "<Grid Height=\"43\" Width=\"779\" Margin=\"5,5,5,5\">" +
                          "<Grid.ColumnDefinitions>" +
                          "<ColumnDefinition Width=\"9*\"/>" +
                          "<ColumnDefinition Width=\"1*\"/>" +
                          "</Grid.ColumnDefinitions>" +
                          "<Grid.RowDefinitions>" +
                          "<RowDefinition Height=\"*\"/>" +
                          "</Grid.RowDefinitions>" +
                          "<ComboBox x:Name=\"cmbInvestigators\" " +
                          "SelectedItem=\"{Binding SelectedInvestigator}\" " +
                          "ItemsSource=\"{Binding Investigators}\">" +
                          "<ComboBox.ItemTemplate>" +
                          "<DataTemplate>" +
                          "<TextBlock>" +
                          "<TextBlock.Text>" +
                          "<MultiBinding StringFormat=\"{}{0} {1}\">" +
                          "<Binding Path=\"LastName\"/>" +
                          "<Binding Path=\"Name\"/>" +
                          "</MultiBinding>" +
                          "</TextBlock.Text>" +
                          "</TextBlock>" +
                          "</DataTemplate>" +
                          "</ComboBox.ItemTemplate>" +
                          "</ComboBox>" +
                          "<StackPanel Grid.Row=\"1\" Grid.Column=\"1\" Orientation=\"Horizontal\" " +
                          "HorizontalAlignment=\"Center\">" +
                          "<Button Content=\"+1\" Margin=\"5,5,5,5\" Background=\"#FF00FF27\" Padding=\"5,5,5,5\"/>" +
                          "<Button Content=\"-1\" Margin=\"5,5,5,5\" Background=\"#FFFF6720\" Padding=\"5,5,5,5\"/>" +
                          "</StackPanel>" +
                          "</Grid>";

            return grid;
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files(*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;.bmp;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    BitmapImage image = new BitmapImage(new Uri(filename));
                    string base64String = ConvertImageToBase64(image);
                    Console.WriteLine(base64String.ToString());
                    Trace.WriteLine(base64String.ToString());
                    Console.WriteLine("sdrgsrgrs");
                    Image imageControl = new Image();
                    imageControl.Width = 200;
                    imageControl.Height = 200;
                    imageControl.Margin = new Thickness(5);
                    imageControl.Source = image;
                    imageControl.Tag = base64String;
                    imagePanel.Children.Add(imageControl);
                }
            }
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
    }
}
