using Lesson1.Models.Interfaces;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lesson1.ViewModels
{
    class LoginPageViewModel : BaseViewModel
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
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                CredentialsChanged();
                RaisePropertyChanged();
            }
        }

        public string Error
        {
            get => error;
            set
            {
                error = value;
                RaisePropertyChanged(nameof(IsErrorVisible));
                RaisePropertyChanged();
            }
        }

        public bool IsErrorVisible => !string.IsNullOrEmpty(Error);

        #endregion

        #region Commands

        public Command LoginCommand { get; set; }

        #endregion

        #region Methods

        private void Login()
        {
            Error = string.Empty;
            if(loginService.TryToLogin(Username, Password))
            {
                navigationService.MoveToMainPage(new MainPageViewModel(Username));
            }
            else
            {
                Error = "Unable to login with specified credentials.";
            }
        }

        private bool IsAbleToPushLogin()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username) && Validation.ValidationService.ValidateEmail(Username);
        }

        protected virtual void CredentialsChanged()
        {
            LoginCommand.ChangeCanExecute();
        }

        #endregion
    }
}
