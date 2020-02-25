using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ProcessTrackerBOMFormat.Configurations;

namespace ProcessTrackerBOMFormat {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private Regex partNumberRegex;
        private Brush defaultPartNumberBorderBrush;

        private CustomConfigBomSection bomConfigurations;

        public MainWindow() {
            InitializeComponent();

            partNumberRegex = new Regex(@"^((?:(?:G|T)\\d{5}(?:(?=-)-\\d{1,3}(?:(?=[A-Z])[A-Z]\\d|)|))|(?:(?:V)?\\d{6,7}Z))$");

            defaultPartNumberBorderBrush = ProductNumber.BorderBrush;

            try {
                bomConfigurations = (CustomConfigBomSection)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION);
                
                foreach (CustomConfigBom bom in bomConfigurations.BomCollection) {
                    Console.WriteLine("Bom Name: " + bom.Name);
                }
            } catch(Exception e) {
                bomConfigurations = null;
                Console.WriteLine("error occured " + e.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            FormBorder.Focus();
        }

        private void ProductNumber_LostFocus(object sender, RoutedEventArgs e) {
            string value = ((TextBox)sender).Text;

            if (partNumberRegex.IsMatch(value)) {
                ((TextBox)sender).BorderBrush = defaultPartNumberBorderBrush;
            }
            else if (value.Length != 0) {
                ((TextBox)sender).BorderBrush = Brushes.Red;
            }
        }
    }
}
