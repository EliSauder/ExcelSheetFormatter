using Caliburn.Micro;
using Formatter.UserInterface.Interfaces;
using Formatter.UserInterface.Models;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Formatter.UserInterface.ViewModels
{
    public class BomFormatDirectorySelectViewModel : Caliburn.Micro.Conductor<object>.Collection.AllActive, IPopUpContent {

        #region Interface Properties
        public string Title => "Select Directories";
        public double? StartingWidth => 650;

        public double? StartingHeight => 500;
        #endregion

        #region Binding Properties
        private string _rootDirectory = "";
        public string RootDirectory {
            get => _rootDirectory;
            set {
                _rootDirectory = value;
                NotifyOfPropertyChange(() => RootDirectory);
                //OnPropertyChanged();
            }
        }

        private string _inputFolder = "";
        public string InputFolder {
            get => _inputFolder;
            set {
                _inputFolder = value;
                NotifyOfPropertyChange("InputFolder");
                //OnPropertyChanged();
            }
        }

        private string _outputFolder = "";
        public string OutputFolder {
            get => _outputFolder;
            set {
                _outputFolder = value;
                NotifyOfPropertyChange("OutputFolder");
                //OnPropertyChanged();
            }
        }
        #endregion

        #region Interface Methods
        public bool CanExit() {
            return true;
            // throw new System.NotImplementedException();
        }

        public DialogResult Exit() {
            return DialogResult.OK;
            //throw new System.NotImplementedException();
        }

        public void IsError() {
            //throw new System.NotImplementedException();
        }
        #endregion

        private FolderSelectorViewModel _rootDirectoryViewModel;
        public FolderSelectorViewModel FolderSelectorViewModel {
            get => _rootDirectoryViewModel;
            set {
                _rootDirectoryViewModel = value;
                NotifyOfPropertyChange(() => FolderSelectorViewModel);
            }
        }

        private IEventAggregator _events;

        public BomFormatDirectorySelectViewModel(Configuration.ConfigurationSectionFiles fileConfig) {
            RootDirectory = fileConfig.RootDirectory;
            InputFolder = fileConfig.InputFolder;
            OutputFolder = fileConfig.OutputFolder;
        }

        #region Browse Event Handling

        public void RootBrowse() {
            this.RootDirectory = browse(this.RootDirectory);
        }

        public void InputBrowse() {
            if (RootDirectory.Length == 0)
                MessageBox.Show("No Root Directory Selected. Please select root directory first.", "Value not set", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                this.InputFolder = browse(this.RootDirectory + this.InputFolder).Replace(RootDirectory, "");
        }

        public void OutputBrowse() {
            if (RootDirectory.Length == 0)
                MessageBox.Show("No Root Directory Selected. Please select root directory first.", "Value not set", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                this.OutputFolder = browse(this.RootDirectory + this.OutputFolder).Replace(RootDirectory, "");
        }

        private string browse(string startingDirectory) {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = startingDirectory;

            dialog.ShowDialog();

            return dialog.SelectedPath;
        }
        #endregion

    }
}
