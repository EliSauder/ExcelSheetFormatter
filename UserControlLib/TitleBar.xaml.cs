using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlLib {
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl {

        #region Properties

        public string TitleText {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(TitleBar), new PropertyMetadata(""));

        public double TitleFontSize {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(TitleBar), new PropertyMetadata((double)20));

        public Visibility MinimizeVisibility {
            get { return (Visibility)GetValue(MinimizeVisibilityProperty); }
            set { SetValue(MinimizeVisibilityProperty, value); }
        }
        public static readonly DependencyProperty MinimizeVisibilityProperty =
            DependencyProperty.Register("MinimizeVisibility", typeof(Visibility), typeof(TitleBar), new PropertyMetadata(Visibility.Visible));

        public Visibility MaximizeVisibility {
            get { return (Visibility)GetValue(MaximizeVisibilityProperty); }
            set { SetValue(MaximizeVisibilityProperty, value); }
        }
        public static readonly DependencyProperty MaximizeVisibilityProperty =
            DependencyProperty.Register("MaximizeVisibility", typeof(Visibility), typeof(TitleBar), new PropertyMetadata(Visibility.Visible));

        public Visibility ExitVisibility {
            get { return (Visibility)GetValue(ExitVisibilityProperty); }
            set { SetValue(ExitVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ExitVisibilityProperty =
            DependencyProperty.Register("ExitVisibility", typeof(Visibility), typeof(TitleBar), new PropertyMetadata(Visibility.Visible));

        public double ButtonWidth {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(double), typeof(TitleBar), new PropertyMetadata((double)50));
        #endregion

        public TitleBar() {
            this.DataContext = this;
            InitializeComponent();
        }

        #region Events
        public event EventHandler Exit = null;
        public event EventHandler Minimize = null;
        public event EventHandler Maximize = null;
        private void Min_Click(object sender, RoutedEventArgs e) {
            Minimize?.Invoke(this, EventArgs.Empty);
        }

        private void Max_Click(object sender, RoutedEventArgs e) {
            Maximize?.Invoke(this, EventArgs.Empty);
        }

        private void Exi_Click(object sender, RoutedEventArgs e) {
            Exit?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
