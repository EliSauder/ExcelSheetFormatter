using Caliburn.Micro;
using Formatter.Configuration;
using Formatter.UserInterface.EventModels;
using Formatter.UserInterface.Interfaces;
using Formatter.Utility;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Formatter.UserInterface.ViewModels {
    public class BomFormatDirectorySelectViewModel : Conductor<object>.Collection.AllActive, IPopUpContent, IHandle<FolderSelectEvent> {

        private const double TEXTBOX_FONT_SIZE = 15;
        private const double LABEL_FONT_SIZE = 15;
        private readonly static GridLength LABEL_WIDTH = new GridLength(110);

        private IEventAggregator _events;
        private IFormatterConfiguration _configurations;

        #region Interface Properties
        public string Title => "Select Directories";
        public string ProcessButtonText => "Save";
        public double? StartingWidth => 650;

        public double? StartingHeight => 500;
        #endregion

        #region Directory Elements
        private FolderSelectorViewModel _rootDirectoryElement;
        public FolderSelectorViewModel RootDirectoryElement {
            get => _rootDirectoryElement;
            set {
                _rootDirectoryElement = value;
                NotifyOfPropertyChange(() => RootDirectoryElement);
            }
        }

        private FolderSelectorViewModel _inputFolderElement;
        public FolderSelectorViewModel InputFolderElement {
            get => _inputFolderElement;
            set {
                _inputFolderElement = value;
                NotifyOfPropertyChange(() => InputFolderElement);
            }
        }

        private FolderSelectorViewModel _outputFolderElement;
        public FolderSelectorViewModel OutputFolderElement {
            get => _outputFolderElement;
            set {
                _outputFolderElement = value;
                NotifyOfPropertyChange(() => OutputFolderElement);
            }
        }


        public Exception Error { get; private set; } = null;

        public bool HasError => Error != null;
        #endregion

        public BomFormatDirectorySelectViewModel(IFormatterConfiguration configurations, IEventAggregator events) {

            _configurations = configurations;

            this.RootDirectoryElement = new FolderSelectorViewModel(events);
            RootDirectoryElement.LabelWidth = LABEL_WIDTH;
            RootDirectoryElement.LabelContent = "Root Directory: ";
            RootDirectoryElement.LabelFontSize = LABEL_FONT_SIZE;
            RootDirectoryElement.TextBoxFontSize = TEXTBOX_FONT_SIZE;
            try {
                RootDirectoryElement.TextBoxContent = configurations.FileConfiguration.RootDirectory;
            } catch (Exception e) {
                throw new System.Xml.XmlException("Error loading configuration. " + e.Message);
            }
            this.InputFolderElement = new FolderSelectorViewModel(events);
            InputFolderElement.RootFolder = RootDirectoryElement;
            InputFolderElement.LabelWidth = LABEL_WIDTH;
            InputFolderElement.LabelContent = "* Input Folder: ";
            InputFolderElement.LabelFontSize = LABEL_FONT_SIZE;
            InputFolderElement.TextBoxFontSize = TEXTBOX_FONT_SIZE;
            InputFolderElement.TextBoxContent = configurations.FileConfiguration.InputFolder;

            this.OutputFolderElement = new FolderSelectorViewModel(events);
            OutputFolderElement.RootFolder = RootDirectoryElement;
            OutputFolderElement.LabelWidth = LABEL_WIDTH;
            OutputFolderElement.LabelContent = "* Output Folder: ";
            OutputFolderElement.LabelFontSize = LABEL_FONT_SIZE;
            OutputFolderElement.TextBoxFontSize = TEXTBOX_FONT_SIZE;
            OutputFolderElement.TextBoxContent = configurations.FileConfiguration.OutputFolder;

            Items.Add(RootDirectoryElement);
            Items.Add(InputFolderElement);
            Items.Add(OutputFolderElement);

            _events = events;
            events.Subscribe(this);
            this.Activated += (object sender, ActivationEventArgs e) => _events.PublishOnUIThread(new FolderSelectedEvent(null));
        }

        #region Interface Methods
        private bool _canProcess = true;
        public bool CanProcess {
            get {
                bool directoriesAreValid = true;

                if (!Directory.Exists(RootDirectoryElement.TextBoxContent)) directoriesAreValid = false;
                if (!Directory.Exists(Path.Combine(RootDirectoryElement.TextBoxContent, InputFolderElement.TextBoxContent))) directoriesAreValid = false;
                if (!Directory.Exists(Path.Combine(RootDirectoryElement.TextBoxContent, OutputFolderElement.TextBoxContent))) directoriesAreValid = false;

                return directoriesAreValid;
            }
        }

        public void ProcessError() {
            System.Windows.MessageBox.Show("Please make sure the folders you have entered are correct.", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult OnProcess() {
            MessageBoxResult result;
            try {
                _configurations.UpdateFileConfigurations(
                    RootDirectoryElement.TextBoxContent,
                    InputFolderElement.TextBoxContent,
                    OutputFolderElement.TextBoxContent);
                result = MessageBoxResult.OK;
            } catch (Exception e) {
                result = System.Windows.MessageBox.Show("Error occured while updating configuration." + e.Message + "\n Would you like to continue?", "Error occured", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            return result;
        }

        public bool CanExit { get; private set; } = true;
        public void ExitError() { }
        public MessageBoxResult OnExit() => MessageBoxResult.OK;

        #endregion

        #region Browse Event Handling
        private string BrowseFolders(string startingDirectory) {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = startingDirectory;

            dialog.ShowDialog();

            return dialog.SelectedPath;
        }

        public void Handle(FolderSelectEvent message) {
            if (!message.Handle) return;
            if (message.Sender.RootFolder != null && message.Sender.RootFolder.TextBoxContent.Length == 0) {
                System.Windows.MessageBox.Show("No root directory selected. Please select root directory first.", "Value not provided", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                string filePath = message.Sender.RootFolder != null ? Path.Combine(message.Sender.RootFolder.TextBoxContent, message.Sender.TextBoxContent) : message.Sender.TextBoxContent;
                string value = BrowseFolders(filePath);
                message.Sender.TextBoxContent =
                    message.Sender.RootFolder != null ?
                    PathExtention.GetRelativePath(message.Sender.RootFolder.TextBoxContent, value) :
                    value;

                _events.PublishOnUIThread(new FolderSelectedEvent(message.Sender));
            }
            message.Handle = false;
        }
        #endregion
    }
}
