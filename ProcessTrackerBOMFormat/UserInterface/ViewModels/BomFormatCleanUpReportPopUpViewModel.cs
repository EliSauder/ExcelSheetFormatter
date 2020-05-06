using Caliburn.Micro;
using Formatter.UserInterface.Interfaces;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Formatter.UserInterface.ViewModels {
    public class BomFormatCleanUpReportPopUpViewModel : Caliburn.Micro.Screen, IPopUpContent {

        private Collection<TreeViewItem> _inputNodes = null;
        private List<TreeViewItem> _rootNodes = null;

        public double? StartingWidth => 600;
        public double? StartingHeight => 400;
        public string Title { get; } = "Cleanup Results";

        public List<TreeViewItem> RootNodes {
            get => _rootNodes;
            set {
                _rootNodes = value;
                NotifyOfPropertyChange(() => RootNodes);
            }
        }

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

        DialogResult IPopUpContent.Exit() => DialogResult.OK;

        public bool CanExit() => true;

        public void IsError() {}
    }
}
