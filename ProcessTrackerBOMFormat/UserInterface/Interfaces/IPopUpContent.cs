using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formatter.UserInterface.Interfaces {

    public interface IPopUpContent {
        string Title { get; }
        double? StartingWidth { get; }
        double? StartingHeight { get; }
        DialogResult Exit();
        bool CanExit();
        void IsError();
    }
}
