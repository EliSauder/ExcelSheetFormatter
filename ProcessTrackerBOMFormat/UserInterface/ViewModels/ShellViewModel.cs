using Caliburn.Micro;
using ProcessTrackerBOMFormat.UserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTrackerBOMFormat.UserInterface.ViewModels {
    public class ShellViewModel : Conductor<object>.Collection.OneActive {

        public ShellViewModel() {
            ActivateItem(new BomFormatViewModel());
        }

    }
}
