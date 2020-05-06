using Caliburn.Micro;
using DocumentFormat.OpenXml.Vml;
using Formatter.Configuration;
using Formatter.UserInterface.EventModels;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.UserInterface.ViewModels {
    public class FolderSelectorViewModel : Screen {

        private IEventAggregator _events;

        public FolderSelectorViewModel RootFolder { get; set; } = null;

        private string _textBoxContent = "";
        public string TextBoxContent {
            get => _textBoxContent;
            set {
                _textBoxContent = value;
                NotifyOfPropertyChange(() => TextBoxContent);
            }
        }

        private string _labelContent = "";
        public string LabelContent {
            get => _labelContent;
            set {
                _labelContent = value;
                NotifyOfPropertyChange(() => LabelContent);
            }
        }

        private double _textBoxFontSize = 20;
        public double TextBoxFontSize {
            get => _textBoxFontSize;
            set {
                _textBoxFontSize = value;
                NotifyOfPropertyChange(() => TextBoxFontSize);
            }
        }

        private double _labelFontSize = 20;
        public double LabelFontSize {
            get => _labelFontSize;
            set {
                _labelFontSize = value;
                NotifyOfPropertyChange(() => LabelFontSize);
            }
        }

        public FolderSelectorViewModel(IEventAggregator events) {
            this._events = events;

        }

        public void Browse() {
            _events.PublishOnUIThread(new FolderSelectEvent(this));
        }

    }
}
