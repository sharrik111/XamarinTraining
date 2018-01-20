using Lesson1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsHelper.ViewModels;

namespace Lesson1.ViewModels
{
    /// <summary>
    /// View model for main page.
    /// </summary>
    class MainPageViewModel : PageViewModel
    {
        #region Fields

        private BaseViewModel generalInfoPageViewModel = null;

        #endregion

        #region Properties

        public BaseViewModel GeneralInfoPageViewModel
        {
            get => generalInfoPageViewModel;
            set
            {
                generalInfoPageViewModel = value;
                OnPropertyChanged();
            }
        }

        public MapPageViewModel MapPageViewModel { get; set; } = new MapPageViewModel();

        #endregion

        #region Life Cycle

        public MainPageViewModel(IUserModel userModel)
        {
            GeneralInfoPageViewModel = new GeneralInfoPageViewModel(userModel);
        }

        #endregion
    }
}
