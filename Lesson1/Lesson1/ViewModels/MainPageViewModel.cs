using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.ViewModels
{
    /// <summary>
    /// View model for main page.
    /// </summary>
    class MainPageViewModel : BaseViewModel
    {
        #region Fields

        private string username;
        private string title;

        #endregion

        #region Properties

        public string Username
        {
            get => username;
            set
            {
                username = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        private bool IsLoggedIn => !string.IsNullOrEmpty(Username);

        #endregion

        #region Life Cycle

        public MainPageViewModel(string username)
        {
            Username = username;
            if(IsLoggedIn)
                Title = $"You're logged in with name '{Username}'";
            else
                Title = "You are still not logged in.";
        }

        #endregion
    }
}
