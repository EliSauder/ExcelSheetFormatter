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

        private string _processButtonText = "";
        public string ProcessButtonText {
            get => _processButtonText;
            set {
                _processButtonText = value;
                NotifyOfPropertyChange(() => ProcessButtonText);
            }
        }

        public MessageBoxResult MessageBoxResult { get; private set; }

        public PopUpViewModel(IPopUpContent conductor) : this(conductor, conductor.Title) { }

        public PopUpViewModel(IPopUpContent conductor, string title) {
            this.ActivateItem(conductor);
            this.DisplayName = title;
            this.WindowHeight = conductor.StartingHeight ?? WindowHeight;
            this.WindowWidth = conductor.StartingWidth ?? WindowWidth;
            ProcessButtonText = conductor.ProcessButtonText;
        }

        public void Minimize() {
            this.CurrentWindowState = WindowState.Minimized;
        }

        public void Maximize() {
            this.CurrentWindowState = this.CurrentWindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public void Exit() {
            if (this.ActiveItem.CanExit) {
                this.MessageBoxResult = this.ActiveItem.OnExit();
                this.TryClose();
            } else if (this.ActiveItem.HasError) {
                this.ActiveItem.ExitError();
            }
        }

        public void Process() {
            if (this.ActiveItem.CanProcess) {
                this.MessageBoxResult = this.ActiveItem.OnProcess();
                this.TryClose();
            } else if (this.ActiveItem.HasError) {
                this.ActiveItem.ProcessError();
            }
        }
    }
}
