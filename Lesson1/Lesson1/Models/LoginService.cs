using Lesson1.Models;
using Lesson1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(LoginService))]
namespace Lesson1.Models
{
    class LoginService : ILoginService
    {
      // TODO: code review
      // make this method async and add delay to simulate web request
        public bool TryToLogin(string username, string password)
        {
            if (App.Current.Properties.ContainsKey(username))
            {
                return password == App.Current.Properties[username].ToString();
            }
            else
            {
                App.Current.Properties[username] = password;
                App.Current.SavePropertiesAsync();
                return true;
            }
        }
    }
}
