using Lesson1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Navigation.Interfaces
{
    public interface INavigationService
    {
        // TODO(Pavel Ostreyko): Ideally the interface must the only method to navigate to specified page.

        void MoveToMainPage(BaseViewModel viewModel);

        void PopMe();
    }
}
