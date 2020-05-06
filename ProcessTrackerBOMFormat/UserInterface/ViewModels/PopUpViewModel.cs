using Caliburn.Micro;
using Formatter.UserInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Formatter.UserInterface.ViewModels {
    public class PopUpViewModel : Conductor<IPopUpContent>.Collection.OneActive {

        private WindowState _currentWindowState = WindowState.Normal;
        private double _windowWidth = 400;
        private double _windowHeight = 300;

        public WindowState CurrentWindowState {
            get => _currentWindowState;
            set {
                _currentWindowState = value;
                NotifyOfPropertyChange("CurrentWindowState");
            }
        }

        public double WindowHeight {
            get => _windowHeight;
            set {
                _windowHeight = value;
                NotifyOfPropertyChange("WindowHeight");
            }
        }

        public double WindowWidth {
            get => _windowWidth;
            set {
                _windowWidth = value;
                NotifyOfPropertyChange("WindowWidth");
            }
        }

        public DialogResult DialogResult { get; private set; }

        public PopUpViewModel(IPopUpContent conductor) : this(conductor, conductor.Title) { }

        public PopUpViewModel(IPopUpContent conductor, string title) {
            this.ActivateItem(conductor);
            this.DisplayName = title;
            this.WindowHeight = conductor.StartingHeight ?? WindowHeight;
            this.WindowWidth = conductor.StartingWidth ?? WindowWidth;
        }

        public void Minimize() {
            this.CurrentWindowState = WindowState.Minimized;
        }

        public void Maximize() {
            this.CurrentWindowState = this.CurrentWindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public void Exit() {
            if (!this.ActiveItem.CanExit()) this.ActiveItem.IsError();
            else {
                this.DialogResult = this.ActiveItem.Exit();
                this.TryClose();
            }
        }
    }
}
