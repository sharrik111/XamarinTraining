using Lesson1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.ViewModels
{
    class GeneralInfoPageViewModel : PageViewModel
    {
        #region Fields

        private IUserModel userModel;
        private string title;

        #endregion

        #region Properties

        public string Username => userModel.Username;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        private bool IsLoggedIn => userModel.IsLoggedIn;

        #endregion

        #region Life Cycle

        public GeneralInfoPageViewModel(IUserModel userModel)
        {
            this.userModel = userModel;
            if (IsLoggedIn)
                Title = $"You're logged in with name '{Username}'";
            else
                Title = "You are still not logged in.";
        }

        #endregion
    }
}
