using Lesson1.Models;
using Lesson1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsHelper.ViewModels;

namespace Lesson1.ViewModels
{
    class LoginPageViewModel : PageViewModel
    {
        #region Life Cycle

        public LoginPageViewModel(ILoginService loginService)
        {
            this.loginService = loginService;
            LoginCommand = new Command(Login, IsAbleToPushLogin);
        }

        #endregion

        #region Fields
        
        private ILoginService loginService;

        private string username;
        private string password;
        private string error;

        #endregion

        #region Properties

        public string Username
        {
            get => username;
            set
            {
                username = value;
                CredentialsChanged();
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                CredentialsChanged();
                OnPropertyChanged();
            }
        }

        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged(nameof(IsErrorVisible));
                OnPropertyChanged();
            }
        }

        public bool IsErrorVisible => !string.IsNullOrEmpty(Error);

        #endregion

        #region Commands

        public Command LoginCommand { get; set; }

        #endregion

        #region Methods

        private async void Login()
        {
            Error = string.Empty;
            if(await loginService.TryToLoginAsync(Username, Password))
            {
                await NavigationService.PushAsync(new MainPageViewModel(new UserModel(Username)));
            }
            else
            {
                Error = "Unable to login with specified credentials.";
            }
        }

        private bool IsAbleToPushLogin()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username) && Helpers.ValidationHelper.ValidateEmail(Username);
        }

        protected virtual void CredentialsChanged()
        {
            LoginCommand.ChangeCanExecute();
        }

        #endregion
    }
}
