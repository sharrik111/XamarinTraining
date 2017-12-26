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
   // TODO: code review
   // don't use MvvmCross with the Xamarin.Forms
   // to start better investigate possibilities of the one framework
   // and than learn another one
    class LoginPageViewModel : BaseViewModel
    {
        #region Life Cycle

        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login, IsAbleToPushLogin);
        }

        #endregion

        #region Fields

        // I don't see any reasons to create model in this case because now we have nothing except of login service.
      // TODO: code review
      // use constructor to inject the services into the target object
        private ILoginService loginService = DependencyService.Get<ILoginService>();

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
                navigationService.PopMe();
                OnClose();
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

        #region Events

        // Currently I do not know how to interact between view models correctly.
        // So to save some time I just created this event.
      // TODO: code review
      // subscribtions for events / delegates often lead to memory leaks
      // use dataContext instead - https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/navigation/hierarchical/#Passing_Data_through_a_BindingContext
        public event EventHandler ViewModelClose;

        protected virtual void OnClose()
        {
            ViewModelClose?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
