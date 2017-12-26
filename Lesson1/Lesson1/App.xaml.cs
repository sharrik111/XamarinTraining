using Lesson1.Models;
using Lesson1.Models.Interfaces;
using Lesson1.Navigation;
using Lesson1.Navigation.Interfaces;
using Lesson1.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Lesson1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
    }
}
