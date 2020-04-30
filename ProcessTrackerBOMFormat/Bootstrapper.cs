using Caliburn.Micro;
using Formatter.Configuration;
using Formatter.UserInterface.ViewModels;
using Formatter.Utility;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter {
    public class Bootstrapper : BootstrapperBase {

        private ConfigurationSectionBoms bomConfigurations;
        private ConfigurationSectionFiles fileConfigurations;

        private static Excel.Application application = null;

        public Bootstrapper() {
            this.Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {

            try {
                ConfigurationSectionBoms bomConfigurations = (ConfigurationSectionBoms)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION);
                ConfigurationSectionFiles fileConfigurations = (ConfigurationSectionFiles)ConfigurationManager.GetSection(Properties.Resources.FILE_CONFIGURATION_SECTION);
                
                foreach(ConfigurationElementBom bom in bomConfigurations.BomCollection) {
                    foreach(ConfigurationElementColumn column in bom.ColumnCollection) {
                        if(column.PopulationCollection != null) {
                            foreach(ConfigurationElementPopulation population in column.PopulationCollection) {
                                if (bom.ColumnCollection[population.ToColumn] == null) throw new InvalidExpressionException("boms." + bom.Name + ".fields." + column.Name + ".populations." + population.Name + ".toColumn references invalid column");
                            }
                        }
                    }
                }
            } catch (Exception error) {
                MessageBox.Show("Configuration Error:\n\n" + ErrorFormating.FormatException(error), "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, s) => CreateExcelInstance();

            worker.RunWorkerAsync();

            DisplayRootViewFor<ShellViewModel>();
        }

        public static void CreateExcelInstance() {
            application = new Excel.Application();
        }

        public static Excel.Application GetExcelInstance() {
            return application;
        }

        public static void ClearOpenWorkbooks() {
            foreach(Excel.Workbook wb in application.Workbooks) {
                wb.Close();
            }
        }

        public static void CloseExcelInstance() {
            if (application != null) try { application.Quit(); } catch(Exception e) { }
        }

        ~Bootstrapper() => CloseExcelInstance();
    }
}
