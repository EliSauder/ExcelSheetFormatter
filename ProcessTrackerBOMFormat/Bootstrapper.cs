using Caliburn.Micro;
using ProcessTrackerBOMFormat.Configuration;
using ProcessTrackerBOMFormat.UserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ProcessTrackerBOMFormat {
    public class Bootstrapper : BootstrapperBase {

        private Regex partNumberRegex;
        private Brush defaultPartNumberBorderBrush;

        private ConfigurationSectionBoms bomConfigurations;

        public Bootstrapper() {

            partNumberRegex = new Regex(@"^((?:(?:G|T)\\d{5}(?:(?=-)-\\d{1,3}(?:(?=[A-Z])[A-Z]\\d|)|))|(?:(?:V)?\\d{6,7}Z))$");

            try {
                bomConfigurations = (ConfigurationSectionBoms)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION);
                this.Initialize();
            }
            catch (Exception e) {
                int indexOfParan = e.Message.IndexOf("(");
                int messageEnd = indexOfParan == -1 ? e.Message.Length : indexOfParan;
                int indexOfLine = e.Message.IndexOf("line");

                string lineNumber = indexOfLine == -1 ? "" : e.Message.Substring(indexOfLine);
                string lineError = indexOfLine == -1 ? "" : lineNumber.Substring(0, lineNumber.Length - 1);

                MessageBox.Show("Configuration Error:\n\n" + e.Message.Substring(0, messageEnd) + (indexOfLine == -1 ? "" : "\n\n") + lineError, "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            this.DisplayRootViewFor<ShellViewModel>();
        }
    }
}
