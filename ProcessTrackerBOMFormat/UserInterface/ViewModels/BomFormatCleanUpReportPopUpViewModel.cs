using Caliburn.Micro;
using Formatter.UserInterface.Interfaces;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Formatter.UserInterface.ViewModels {
    public class BomFormatCleanUpReportPopUpViewModel : Caliburn.Micro.Screen, IPopUpContent {

        private Collection<TreeViewItem> _inputNodes = null;
        private List<TreeViewItem> _rootNodes = null;

        public string ProcessButtonText => "Continue";
        public double? StartingWidth => 600;
        public double? StartingHeight => 400;
        public string Title => "Cleanup Results";

        public List<TreeViewItem> RootNodes {
            get => _rootNodes;
            set {
                _rootNodes = value;
                NotifyOfPropertyChange(() => RootNodes);
            }
        }

        public Exception Error => null;

        public bool HasError => false;

        public BomFormatCleanUpReportPopUpViewModel(Collection<TreeViewItem> items) {

            RootNodes = new List<TreeViewItem>();
            _inputNodes = items;

            Activate();
        }

        public void Activate() {

            List<TreeViewItem> nodes = new List<TreeViewItem>();

            foreach (TreeViewItem item in _inputNodes) nodes.Add(item);

            RootNodes = nodes;
        }

        public MessageBoxResult OnExit() => MessageBoxResult.OK;

        public bool CanExit => true;

        public MessageBoxResult OnProcess() => OnExit();

        public bool CanProcess => CanExit;

        public void ExitError() {
            //No implementation needed
        }
        public void ProcessError() => ExitError();
    }
}
