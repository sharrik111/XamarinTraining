using Lesson1.Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lesson1.ViewModels;
using Lesson1.Views;
using Lesson1.Navigation;

[assembly: Xamarin.Forms.Dependency(typeof(NavigationService))]
namespace Lesson1.Navigation
{
    /// <summary>
    /// Simple navigation service to save some time.
    /// TODO(Pavel Ostreyko): Ideally the implementation must take needed pages depending on view model type using just one method.
    /// </summary>
    public class NavigationService : INavigationService
    {
        public void MoveToMainPage(BaseViewModel viewModel)
        {
            App.Current.MainPage.Navigation.PushAsync(new MainPage { BindingContext = viewModel });
        }

        public void PopMe()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
