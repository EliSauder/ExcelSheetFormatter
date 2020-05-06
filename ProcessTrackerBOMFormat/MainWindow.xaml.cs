using Formatter.Configuration;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Formatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Regex partNumberRegex;
        private Brush defaultPartNumberBorderBrush;

        private ConfigurationSectionBoms bomConfigurations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'MainWindow.MainWindow()'
        public MainWindow()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'MainWindow.MainWindow()'
            InitializeComponent();

            partNumberRegex = new Regex(@"^((?:(?:G|T)\\d{5}(?:(?=-)-\\d{1,3}(?:(?=[A-Z])[A-Z]\\d|)|))|(?:(?:V)?\\d{6,7}Z))$");

            defaultPartNumberBorderBrush = ProductNumber.BorderBrush;

            try
            {
                bomConfigurations = (ConfigurationSectionBoms)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION);
            }
            catch (Exception e)
            {
                int indexOfParan = e.Message.IndexOf("(");
                int messageEnd = indexOfParan == -1 ? e.Message.Length : indexOfParan;
                int indexOfLine = e.Message.IndexOf("line");

                string lineNumber = indexOfLine == -1 ? "" : e.Message.Substring(indexOfLine);
                string lineError = indexOfLine == -1 ? "" : lineNumber.Substring(0, lineNumber.Length - 1);

                MessageBox.Show("Configuration Error:\n\n" + e.Message.Substring(0, messageEnd) + (indexOfLine == -1 ? "" : "\n\n") + lineError, "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FormBorder.Focus();
        }

        private void ProductNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            string value = ((TextBox)sender).Text;

            if (partNumberRegex.IsMatch(value))
            {
                ((TextBox)sender).BorderBrush = defaultPartNumberBorderBrush;
            }
            else if (value.Length != 0)
            {
                ((TextBox)sender).BorderBrush = Brushes.Red;
            }
        }
    }
}
