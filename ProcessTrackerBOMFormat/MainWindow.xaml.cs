using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ProcessTrackerBOMFormat {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private Regex partNumberRegex;
        private Brush defaultPartNumberBorderBrush;

        public MainWindow() {
            InitializeComponent();

            partNumberRegex = new Regex("^((?:(?:G|T)\\d{5}(?:(?=-)-\\d{1,3}(?:(?=[A-Z])[A-Z]\\d|)|))|(?:(?:V)?\\d{6,7}Z))$");

            defaultPartNumberBorderBrush = ProductNumber.BorderBrush;
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
