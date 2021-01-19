using Formatter.UserInterface.ViewModels;
using System.Windows.Controls;

namespace Formatter.UserInterface.Views {
    /// <summary>
    /// Interaction logic for BomFormatDirectorySelect.xaml
    /// </summary>
    public partial class BomFormatDirectorySelectView : UserControl {
        public BomFormatDirectorySelectView() {
            InitializeComponent();
        }

        private void DirectorySelect_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            BomFormatDirectorySelectViewModel viewModel = (BomFormatDirectorySelectViewModel)this.DataContext;

            //this.RootElement.TextBoxContent = viewModel.RootDirectory;
            //this.InputElement.TextBoxContent = viewModel.InputFolder;
            //this.OutputElement.TextBoxContent = viewModel.OutputFolder;
        }
    }
}
