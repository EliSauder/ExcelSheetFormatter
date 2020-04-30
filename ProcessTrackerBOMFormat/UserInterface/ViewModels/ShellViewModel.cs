using Caliburn.Micro;
using System.ComponentModel.Composition;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter.UserInterface.ViewModels {

    [Export]
    public class ShellViewModel : Conductor<object>.Collection.OneActive {

        private Excel.Application excelApp = null;

        public ShellViewModel() {
            excelApp = Bootstrapper.GetExcelInstance();
            ActivateItem(new BomFormatFormViewModel());
        }

    }
}
