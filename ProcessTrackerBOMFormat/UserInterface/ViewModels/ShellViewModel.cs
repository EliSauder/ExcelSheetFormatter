using Caliburn.Micro;
using System.ComponentModel.Composition;
using Excel = Microsoft.Office.Interop.Excel;

namespace Formatter.UserInterface.ViewModels {

    public class ShellViewModel : Conductor<object>.Collection.OneActive {

        private Excel.Application excelApp = null;

        public ShellViewModel(SimpleContainer container) {
            excelApp = Bootstrapper.GetExcelInstance();
            ActivateItem(container.GetInstance<BomFormatFormViewModel>());
        }
    }
}
