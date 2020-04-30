using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Formatter.UserInterface.ViewModels {
    public class PopUpViewModel : Screen {


        private Collection<TreeViewItem> _inputNodes = null;
        private List<TreeViewItem> _rootNodes = null;
        public List<TreeViewItem> RootNodes {
            get {
                return _rootNodes;
            }
            set {
                _rootNodes = value;
                NotifyOfPropertyChange(() => RootNodes);
            }
        }

        public PopUpViewModel(Collection<TreeViewItem> items) {

            RootNodes = new List<TreeViewItem>();
            _inputNodes = items;

            Activate();
        }

        public void Exit() {
            this.TryClose();
        }

        public void Continue() {
            this.Exit();
        }

        public void Activate() {

            List<TreeViewItem> nodes = new List<TreeViewItem>();

            foreach (TreeViewItem item in _inputNodes) {
                nodes.Add(item);
            }

            RootNodes = nodes;
        }
    }
}
