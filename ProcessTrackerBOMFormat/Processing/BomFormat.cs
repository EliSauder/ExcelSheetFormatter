using Caliburn.Micro;
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

namespace Formatter.Processing {
    public class BomFormat {

        private ProductNumberModel productNumber = null;
        private ConfigurationSectionFiles fileConfigurations = null;
        private ConfigurationElementBom bomConfiguration = null;

        private string inputFolder = "";
        private string inputExtention = "";

        private BomOutputType bomOutputType;

        public BomFormat(ProductNumberModel productNumber, ConfigurationSectionFiles fileConfigurations, ConfigurationElementBom bomConfiguration, BomOutputType bomOutputType) {
            this.productNumber = productNumber;
            this.fileConfigurations = fileConfigurations;
            this.bomConfiguration = bomConfiguration;

            this.inputFolder = fileConfigurations.RootDirectory + fileConfigurations.InputDirectory;
            this.inputExtention = bomConfiguration.InputFileExtention;

            this.bomOutputType = bomOutputType;
        }

        public void FormatBom() {

            StringBuilder outputFileName = new StringBuilder();

            outputFileName.Append(fileConfigurations.RootDirectory)
                .Append(fileConfigurations.OutputFolder)
                .Append(productNumber.ProductNumber)
                .Append("-output")
                .Append(Properties.Resources.OUTPUTFILE_EXTENTION);

            try {
                File.OpenWrite(outputFileName.ToString()).Close();
            } catch (Exception e) {
                throw new FileNotFoundException("Unable to open file: " + outputFileName.ToString() + "\nCheck to see if you have it open.");
            }

            Stopwatch s = new Stopwatch();

            s.Start();

            while (Bootstrapper.GetExcelInstance() == null && s.ElapsedMilliseconds < 10000) ;

            s.Stop();

            if(Bootstrapper.GetExcelInstance() == null)
                throw new ArgumentNullException("Unable to open instance of excel.");

            Bootstrapper.ClearOpenWorkbooks();

            BomInput bomInput = new BomInput(productNumber, inputFolder, inputExtention);
            BomOutput bomOutput = new BomOutput(Bootstrapper.GetExcelInstance(), bomInput, bomConfiguration, fileConfigurations) ;
            new BomLoad(bomConfiguration, bomOutputType, bomInput, bomOutput);
            BomPopulations bomPopulations = new BomPopulations(bomOutput, bomConfiguration.ColumnCollection);
            BomCleanup bomCleanup = new BomCleanup(bomConfiguration.ColumnCollection, bomPopulations);

            bomOutput.CopyDataToExcel();

            WindowManager windowManager = new WindowManager();

            Collection<TreeViewItem> items = bomCleanup.OutputResults();

            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.None;
            settings.ShowInTaskbar = false;
            settings.Title = "Cleanup Results";

            bomOutput.SaveWorkbook();

            windowManager.ShowDialog(new PopUpViewModel(items), null, settings);

            Bootstrapper.GetExcelInstance().Visible = true;
        }



    }
}
