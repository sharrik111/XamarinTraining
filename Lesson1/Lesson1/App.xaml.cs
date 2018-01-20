using Lesson1.Models;
using Lesson1.Models.Interfaces;
using Lesson1.Models.Login;
using Lesson1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinFormsHelper.Navigation;

namespace Lesson1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterNavigations();
            RegisterDependencies();

            MainPage = new NavigationPage(new Lesson1.Views.LoginPage());
        }

        protected override void OnStart()
        {
            var loginViewModel = new LoginPageViewModel(DependencyService.Get<ILoginService>());
            MainPage.BindingContext = loginViewModel;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterDependencies()
        {
            // DependencyService.Register<ILoginService, ApplicationLoginService>();
            DependencyService.Register<ILoginService, XamarinSuggestedLoginService>();
            // DependencyService.Register<ILoginService, AkavacheLoginService>();
            // DependencyService.Register<ILoginService, SQLiteLoginService>();
        }

        private void RegisterNavigations()
        {
            // Make sure it's a global application instance.
            var service = DependencyService.Get<INavigationService>();
            service.RegisterNavigation<Views.MainPage, MainPageViewModel>();
        }

        ~App()
        {
            // Not sure it'll be working but currently it doesn't matter.
            // BlobCache.Shutdown().Wait();
        }
    }
}
