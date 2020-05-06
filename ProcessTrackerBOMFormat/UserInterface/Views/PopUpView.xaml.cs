using Formatter.UserInterface.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Formatter.UserInterface.Views
{
    /// <summary>
    /// Interaction logic for PopUpView.xaml
    /// </summary>
    public partial class PopUpView : Window {
        public PopUpView() {
            InitializeComponent();
        }

        private void popUpTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if (this.WindowState == WindowState.Maximized) ((PopUpViewModel)this.DataContext).Maximize();
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
    }
}
