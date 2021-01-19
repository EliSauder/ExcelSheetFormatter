using Caliburn.Micro;
using DocumentFormat.OpenXml.Spreadsheet;
using Formatter.Configuration;
using Formatter.UserInterface.Models;
using Formatter.UserInterface.ViewModels;
using Formatter.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Formatter.Processing {
    public class BomFormat {

        private IFormatterConfiguration _formatterConfiguration;
        private string _selectedBom;
        private ProductNumberModel _productNumberModel;

        public BomFormat(IFormatterConfiguration formatterConfiguration, string selectedBom, ProductNumberModel productNumber) {
            this._formatterConfiguration = formatterConfiguration;
            this._selectedBom = selectedBom;
            this._productNumberModel = productNumber;
        }

        public void FormatBom() {
            string outputFilePath = Path.Combine(_formatterConfiguration.OutputFolderPath, _productNumberModel.ProductNumber + "-output");
            outputFilePath = Path.GetFullPath(Path.ChangeExtension(outputFilePath, Properties.Resources.OUTPUTFILE_EXTENTION));

            ConfigurationElementBom selectedBomConfig = _formatterConfiguration.BomConfiguration.BomCollection[_selectedBom];

            string inputFilePath = Path.Combine(_formatterConfiguration.InputFolderPath, _productNumberModel.ProductNumber);
            inputFilePath = Path.GetFullPath(Path.ChangeExtension(inputFilePath, selectedBomConfig.InputFileExtention));

            if (!File.Exists(inputFilePath)) throw new FileNotFoundException("File not found: " + inputFilePath + "\nMake sure the directory configurations are correct and that the files are named correctly and located in the right directory.");

            try {
                File.OpenWrite(outputFilePath).Close();
            } catch (Exception) {
                throw new FileNotFoundException("Unable to open file: " + outputFilePath + "\nCheck to see if you have it open.");
            }

            Stopwatch s = new Stopwatch();

            s.Start();

            while (Bootstrapper.GetExcelInstance() == null && s.ElapsedMilliseconds < 10000) ;

            s.Stop();

            if (Bootstrapper.GetExcelInstance() == null)
                throw new ArgumentNullException("Unable to open instance of excel.");

            Bootstrapper.ClearOpenWorkbooks();

            BomInput bomInput = new BomInput(_productNumberModel, _formatterConfiguration.InputFolderPath, selectedBomConfig.InputFileExtention, selectedBomConfig);
            BomOutput bomOutput = new BomOutput(Bootstrapper.GetExcelInstance(), bomInput, selectedBomConfig, _formatterConfiguration);
            new BomLoad(selectedBomConfig, bomInput, bomOutput);
            BomPopulations bomPopulations = new BomPopulations(bomOutput, selectedBomConfig);
            BomCleanup bomCleanup = new BomCleanup(selectedBomConfig, bomPopulations);

            bomOutput.CopyDataToExcel();

            WindowManager windowManager = new WindowManager();

            Collection<TreeViewItem> items = bomCleanup.OutputResults();

            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.None;
            settings.ShowInTaskbar = false;
            settings.Title = "Cleanup Results";

            bomOutput.SaveWorkbook();

            BomFormatCleanUpReportPopUpViewModel cleanupViewModel = new BomFormatCleanUpReportPopUpViewModel(items);

            windowManager.ShowDialog(new PopUpViewModel(cleanupViewModel), null, settings);

            Bootstrapper.GetExcelInstance().Visible = true;
        }
    }
}
