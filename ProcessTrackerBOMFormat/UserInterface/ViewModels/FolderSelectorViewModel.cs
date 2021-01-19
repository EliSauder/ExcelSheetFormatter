using Caliburn.Micro;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml;
using Formatter.UserInterface.EventModels;
using System.IO;
using System.Windows;
using System.Windows.Media;
using UserControlLib;

namespace Formatter.UserInterface.ViewModels {
    public class FolderSelectorViewModel : Screen, IHandle<FolderSelectedEvent> {

        private IEventAggregator _events;
        public FolderSelectorViewModel RootFolder { get; set; } = null;

        private readonly static Brush DEFAULT_BORDER_BRUSH = (Brush)(new BrushConverter().ConvertFromString("#444"));
        private readonly static Brush ERROR_BORDER_BRUSH = Brushes.Red;

        private Brush _borderBrush = DEFAULT_BORDER_BRUSH;
        public Brush BorderBrush {
            get => _borderBrush;
            set {
                _borderBrush = value;
                NotifyOfPropertyChange(() => BorderBrush);
            }
        }

        private string _textBoxContent = "";
        public string TextBoxContent {
            get => _textBoxContent;
            set {
                _textBoxContent = value;
                NotifyOfPropertyChange(() => TextBoxContent);
                ValidateTextBox();
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

        private GridLength _labelWidth = GridLength.Auto;
        public GridLength LabelWidth {
            get => _labelWidth;
            set {
                _labelWidth = value;
                NotifyOfPropertyChange(() => LabelWidth);
            }
        }
        public FolderSelectorViewModel(IEventAggregator events) {
            this._events = events;
            _events.Subscribe(this);
        }

        public void Browse() {
            _events.PublishOnUIThread(new FolderSelectEvent(this));
        }

        private void ValidateTextBox() {
            string directory =
                this.RootFolder != null ?
                System.IO.Path.Combine(RootFolder.TextBoxContent, TextBoxContent) :
                TextBoxContent;
            if (!Directory.Exists(directory)) this.BorderBrush = ERROR_BORDER_BRUSH;
            else this.BorderBrush = DEFAULT_BORDER_BRUSH;
        }

        public void Handle(FolderSelectedEvent message) {
            if (message != null && !message.Handle) return;

            ValidateTextBox();

            if (message != null) message.Handle = false;
        }
    }
}
