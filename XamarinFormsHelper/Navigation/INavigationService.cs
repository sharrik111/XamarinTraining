using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsHelper.ViewModels;

namespace XamarinFormsHelper.Navigation
{
    public interface INavigationService
    {
        void RegisterNavigation<TPage, TViewModel>() where TPage : Page, new()
                                                     where TViewModel : BaseViewModel;

        bool IsRegistered<TViewModel>() where TViewModel : BaseViewModel;

        Task PushAsync(BaseViewModel viewModel);

        Task PopAsync();

        Task PopModalAsync();

        Task PushModalAsync(BaseViewModel viewModel);

        Task PopToRootAsync();

        Task PushAsync(BaseViewModel viewModel, bool animated);

        Task PopAsync(bool animated);

        Task PopModalAsync(bool animated);

        Task PushModalAsync(BaseViewModel viewModel, bool animated);

        void InsertPageBefore(BaseViewModel viewModelPage, BaseViewModel viewModelBefore);

        Task PopToRootAsync(bool animated);

        void RemovePage(BaseViewModel viewModel);
    }
}
