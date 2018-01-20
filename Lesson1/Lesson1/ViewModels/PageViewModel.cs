using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsHelper.Navigation;
using XamarinFormsHelper.ViewModels;

namespace Lesson1.ViewModels
{
    class PageViewModel : BaseViewModel
    {
        protected INavigationService NavigationService { get; } = DependencyService.Get<INavigationService>();
    }
}
