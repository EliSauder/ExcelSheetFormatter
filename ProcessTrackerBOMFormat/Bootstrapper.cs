using Caliburn.Micro;
using Formatter.Configuration;
using Formatter.UserInterface.ViewModelFactories;
using Formatter.UserInterface.ViewModels;
using Formatter.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter {
    public class Bootstrapper : BootstrapperBase {

        private const string VIEWMODEL_SUFFIX = "ViewModel";

        private static Excel.Application _application = null;
        private readonly SimpleContainer _containter = new SimpleContainer();

        public Bootstrapper() {
            this.Initialize();
        }

        protected override void Configure() {
            _containter.Instance(_containter);
            _containter
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            _containter
                .PerRequest<IFormatterConfiguration, FormatterConfiguration>()
                .PerRequest<IFactory<PopUpViewModel>, PopUpViewModelFactory>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith(VIEWMODEL_SUFFIX))
                .ToList()
                .ForEach(viewModelType => _containter.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {

            try {
                IFormatterConfiguration configurations = _containter.GetInstance<IFormatterConfiguration>();

                ConfigurationSectionBoms bomConfigurations = configurations.BomConfiguration;
                ConfigurationSectionFiles fileConfigurations = configurations.FileConfiguration;

                foreach (ConfigurationElementBom bom in bomConfigurations.BomCollection) {
                    foreach (ConfigurationElementColumn column in bom.ColumnCollection) {
                        if (column.PopulationCollection != null) {
                            foreach (ConfigurationElementPopulation population in column.PopulationCollection) {
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
            //DisplayRootViewFor<BomFormatDirectorySelectViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            return this._containter.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return this._containter.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            _containter.BuildUp(instance);
        }

        protected override void OnExit(object sender, EventArgs e) {
            base.OnExit(sender, e);
        }

        public static void CreateExcelInstance() {
            _application = new Excel.Application();
            CloseExcelInstance();
        }

        public static Excel.Application GetExcelInstance() {
            return _application;
        }

        public static void ClearOpenWorkbooks() {
            foreach (Excel.Workbook wb in _application.Workbooks) {
                wb.Close(0);
            }
        }

        public static void CloseExcelInstance() {
            if (_application != null) try {
                    ClearOpenWorkbooks();
                    _application.Quit(); 
                } catch (Exception) { }
        }

        ~Bootstrapper() => CloseExcelInstance();
    }
}
