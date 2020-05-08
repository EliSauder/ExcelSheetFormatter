using Formatter.UserInterface.ViewModels;
using Formatter.UserInterface.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.UserInterface.EventModels {
    public class FolderSelectedEvent {
        public FolderSelectorViewModel Sender { get; }
        public bool Handle = true;

        public FolderSelectedEvent(FolderSelectorViewModel sender) {
            Sender = sender;
        }
    }
}
