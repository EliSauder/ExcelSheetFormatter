using Formatter.UserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.UserInterface.EventModels {
    public class FolderSelectEvent {

        public FolderSelectorViewModel Sender { get; }

        public FolderSelectEvent(FolderSelectorViewModel sender) {
            this.Sender = sender;
        }

    }
}
