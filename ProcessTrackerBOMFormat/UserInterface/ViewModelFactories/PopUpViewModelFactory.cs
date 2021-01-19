using Formatter.UserInterface.Interfaces;
using Formatter.UserInterface.ViewModels;
using Formatter.UserInterface.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.UserInterface.ViewModelFactories {
    public class PopUpViewModelFactory : IFactory<PopUpViewModel> {

        public PopUpViewModel Create(params object[] args) {

            if (args.Length < 1 || args.Length > 2) throw new ArgumentException("Invalid number of parameters. " + typeof(PopUpViewModel).FullName + " expects 2 or 3 parameters");

            if (!typeof(IPopUpContent).IsAssignableFrom(args[0].GetType())) throw new ArgumentException("Argument 0 is not of type " + typeof(IPopUpContent).FullName);

            if (args.Length == 2 && args[1].GetType() != typeof(string)) throw new ArgumentException("Argument 1 is not of type " + typeof(string).FullName);

            return args.Length == 1 ? new PopUpViewModel((IPopUpContent)args[0]) : new PopUpViewModel((IPopUpContent)args[0], (string)args[1]);
        }
    }
}
