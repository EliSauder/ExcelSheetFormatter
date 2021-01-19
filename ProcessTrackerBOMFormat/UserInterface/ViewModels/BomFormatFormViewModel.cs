using Caliburn.Micro;
using Formatter.Configuration;
using Formatter.Processing;
using Formatter.UserInterface.Models;
using Formatter.UserInterface.ViewModelFactories;
using Formatter.Utility;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Formatter.UserInterface.ViewModels {
    public class BomFormatFormViewModel : Screen, IBomFormatChild {
        private ProductNumberModel _productNumber = null;

        public string ProductNumber {
            get { return _productNumber.ProductNumber; }
            set {
                _productNumber.ProductNumber = value;
                NotifyOfPropertyChange(() => ProductNumber);
            }
        }

        public BomSelectionModel BomSelectionModel { get; }

        public Dictionary<string, string> Boms {
            get { return BomSelectionModel.Boms; }
        }

        public KeyValuePair<string, string> SelectedItem {
            get { return BomSelectionModel.SelectedItem; }
            set {
                BomSelectionModel.SelectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        private IWindowManager _windowManager;
        private SimpleContainer _container;
        private IFactory<PopUpViewModel> _popUpViewModelFactory;

        public BomFormatFormViewModel(IWindowManager windowManager, SimpleContainer container, IFactory<PopUpViewModel> popUpViewFactory) {
            this._windowManager = windowManager;
            this._container = container;
            _popUpViewModelFactory = popUpViewFactory;

            IFormatterConfiguration config = _container.GetInstance<IFormatterConfiguration>();

            _productNumber = new ProductNumberModel(config);
            BomSelectionModel = new BomSelectionModel(config);

            Activated += (object sender, ActivationEventArgs e) => {
                IFormatterConfiguration configuration = _container.GetInstance<IFormatterConfiguration>();
                if (configuration.FileConfiguration.RootDirectory.Length == 0) FolderSelect();
            };
        }

        public void Process() {
            if (!_productNumber.validateProductNumber()) {
                MessageBox.Show("Invalid Product number.", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else if (!BomSelectionModel.HasSelectedItem()) {
                MessageBox.Show("No BOM is selected.", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try {
                ConfigurationManager.RefreshSection("applicationConfiguration");

                BomFormat bomFormat = new BomFormat(_container.GetInstance<IFormatterConfiguration>(), BomSelectionModel.SelectedItem.Key, _productNumber);

                bomFormat.FormatBom();

                ProductNumber = "";

            } catch (Exception e) {
                MessageBox.Show("Error:\n\n" + ErrorFormating.FormatException(e), "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void FolderSelect() {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.None;
            settings.ShowInTaskbar = true;

            BomFormatDirectorySelectViewModel directorySelectViewModel = _container.GetInstance<BomFormatDirectorySelectViewModel>();

            _windowManager.ShowDialog(_popUpViewModelFactory.Create(directorySelectViewModel, "Directory Configuration"), null, settings);
        }
    }
}
