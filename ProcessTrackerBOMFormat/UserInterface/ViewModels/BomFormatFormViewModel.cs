using Caliburn.Micro;
using Formatter.Configuration;
using Formatter.Processing;
using Formatter.UserInterface.Models;
using Formatter.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Windows;

namespace Formatter.UserInterface.ViewModels
{
    public class BomFormatFormViewModel : Screen, IBomFormatChild {

        private BomSelectionModel _bomSelectionModel = new BomSelectionModel();
        private ProductNumberModel _productNumber = new ProductNumberModel();

        public string ProductNumber {
            get { return _productNumber.ProductNumber; }
            set {
                _productNumber.ProductNumber = value;
                NotifyOfPropertyChange(() => ProductNumber);
            }
        }

        public BomSelectionModel BomSelectionModel {
            get { return _bomSelectionModel; }
        }

        public Dictionary<string, string> Boms {
            get { return _bomSelectionModel.Boms; }
        }

        public KeyValuePair<string, string> SelectedItem {
            get { return _bomSelectionModel.SelectedItem; }
            set {
                _bomSelectionModel.SelectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public void Process() {
            if (!_productNumber.validateProductNumber()) {
                MessageBox.Show("Invalid Product number.", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else if (!_bomSelectionModel.HasSelectedItem()) {
                MessageBox.Show("No BOM is selected.", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //try
            //{
            ConfigurationManager.RefreshSection("applicationConfiguration");

            ConfigurationSectionFiles fileConfig = (ConfigurationSectionFiles)ConfigurationManager.GetSection(Properties.Resources.FILE_CONFIGURATION_SECTION);
            ConfigurationSectionBoms bomConfigs = (ConfigurationSectionBoms)ConfigurationManager.GetSection(Properties.Resources.BOM_CONFIGURATION_SECTION);
            ConfigurationElementBom bomConfig = bomConfigs.BomCollection[_bomSelectionModel.SelectedItem.Key];

            BomFormat bomFormat = new BomFormat(
                _productNumber,
               fileConfig,
               bomConfig,
               bomConfig.OutputType);

            bomFormat.FormatBom();

            ProductNumber = "";

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Error:\n\n" + ErrorFormating.FormatException(e), "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        public void FolderSelect() {
            WindowManager windowManager = new WindowManager();

            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.None;
            settings.ShowInTaskbar = false;

            ConfigurationSectionFiles fileConfig = (ConfigurationSectionFiles)ConfigurationManager.GetSection(Properties.Resources.FILE_CONFIGURATION_SECTION);

            BomFormatDirectorySelectViewModel directorySelectViewModel = new BomFormatDirectorySelectViewModel(fileConfig);

            windowManager.ShowDialog(new PopUpViewModel(directorySelectViewModel), null, settings);
        }
    }
}
