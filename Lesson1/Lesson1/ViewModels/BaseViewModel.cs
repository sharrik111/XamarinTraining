using Lesson1.Navigation.Interfaces;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.ViewModels
{
    /// <summary>
    /// Simple base view model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService navigationService = Mvx.Resolve<INavigationService>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
