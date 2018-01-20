using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsHelper.ViewModels;

[assembly: Dependency(typeof(XamarinFormsHelper.Navigation.NavigationService))]
namespace XamarinFormsHelper.Navigation
{
    /// <summary>
    /// The service which helps to perform view-model first navigation without using reflection to search the corresponding page.
    /// </summary>
    /// <seealso cref="XamarinFormsHelper.Navigation.INavigationService" />
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> registrations = new Dictionary<Type, Type>();

        #region INavigationService

        public void RegisterNavigation<TPage, TViewModel>() where TPage : Page, new()
                                                            where TViewModel : BaseViewModel
        {
            registrations[typeof(TViewModel)] = typeof(TPage);
        }

        public bool IsRegistered<TViewModel>() where TViewModel : BaseViewModel
        {
            return registrations.ContainsKey(typeof(TViewModel));
        }

        public Task PushAsync(BaseViewModel viewModel)
        {
            return PushAsync(viewModel, true);
        }

        public Task PopAsync()
        {
            return PopAsync(true);
        }

        public Task PopModalAsync()
        {
            return PopModalAsync(true);
        }

        public Task PushModalAsync(BaseViewModel viewModel)
        {
            return PushModalAsync(viewModel, true);
        }

        public Task PopToRootAsync()
        {
            return PopToRootAsync(true);
        }

        public async Task PushAsync(BaseViewModel viewModel, bool animated)
        {
            var pageType = GetRegistration(viewModel.GetType());
            var pageInstance = CreatePageOfType(pageType, viewModel);
            await Application.Current.MainPage.Navigation.PushAsync(pageInstance, animated);
        }

        public async Task PopAsync(bool animated)
        {
            await Application.Current.MainPage.Navigation.PopAsync(animated);
        }

        public async Task PopModalAsync(bool animated)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(animated);
        }

        public async Task PushModalAsync(BaseViewModel viewModel, bool animated)
        {
            var pageType = GetRegistration(viewModel.GetType());
            var pageInstance = CreatePageOfType(pageType, viewModel);
            await Application.Current.MainPage.Navigation.PushModalAsync(pageInstance, animated);
        }

        public void InsertPageBefore(BaseViewModel viewModelPage, BaseViewModel viewModelBefore)
        {
            var pageType = GetRegistration(viewModelPage.GetType());
            
            Page beforePage = GetFromNavigationStack(viewModelBefore);
            if (beforePage == null)
                throw new InvalidOperationException($"There is no existing page in navigation stack with {nameof(viewModelBefore)} set as binding context.");
            
            Application.Current.MainPage.Navigation.InsertPageBefore(CreatePageOfType(pageType, viewModelPage), beforePage);
        }

        public async Task PopToRootAsync(bool animated)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync(animated);
        }

        public void RemovePage(BaseViewModel viewModel)
        {
            Application.Current.MainPage.Navigation.RemovePage(GetFromNavigationStack(viewModel));
        }

        #endregion // INavigationService

        #region Helper methods

        private Type GetRegistration(Type viewModelType)
        {
            return registrations[viewModelType];
        }

        private Page CreatePageOfType(Type pageType, BaseViewModel bindingContext = null)
        {
            // Unfortunately I don't see any opportunities to avoid reflection in this place.
            // Inspite of we have new() constraint we can't create an instance with 'new' statement.
            Page pageInstance = (Page)Activator.CreateInstance(pageType);
            pageInstance.BindingContext = bindingContext;
            return pageInstance;
        }

        private Page GetFromNavigationStack(BaseViewModel viewModel)
        {
            var existingPageType = GetRegistration(viewModel.GetType());
            foreach (Page stackPage in Application.Current.MainPage.Navigation.NavigationStack)
            {
                if (stackPage.GetType() == existingPageType && ReferenceEquals(stackPage.BindingContext, viewModel))
                    return stackPage;
            }
            return null;
        }

        #endregion // Helper methods
    }
}
