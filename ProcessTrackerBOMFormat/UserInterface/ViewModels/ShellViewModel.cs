using Caliburn.Micro;
using System.ComponentModel.Composition;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter.UserInterface.ViewModels
{

    [Export]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ShellViewModel'
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ShellViewModel'

        private Excel.Application excelApp = null;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ShellViewModel.ShellViewModel()'
        public ShellViewModel()
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ShellViewModel.ShellViewModel()'
            excelApp = Bootstrapper.GetExcelInstance();
            ActivateItem(new BomFormatFormViewModel());
        }

    }
}
