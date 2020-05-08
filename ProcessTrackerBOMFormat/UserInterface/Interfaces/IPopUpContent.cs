using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Formatter.UserInterface.Interfaces {

    public interface IPopUpContent {
        string Title { get; }
        string ProcessButtonText { get; }
        double? StartingWidth { get; }
        double? StartingHeight { get; }
        bool CanExit { get; }
        void ExitError();
        MessageBoxResult OnExit();
        bool CanProcess { get; }
        void ProcessError();
        MessageBoxResult OnProcess();
        Exception Error { get; }
        bool HasError { get; }
    }
}
