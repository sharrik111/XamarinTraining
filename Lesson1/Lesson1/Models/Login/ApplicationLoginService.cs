using Lesson1.Models.Interfaces;
using Lesson1.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationLoginService))]
namespace Lesson1.Models.Login
{
    class ApplicationLoginService : ILoginService
    {
        public async Task<bool> TryToLoginAsync(string username, string password)
        {
            await Task.Delay(3000);
            if (App.Current.Properties.ContainsKey(username))
            {
                return password == App.Current.Properties[username].ToString();
            }
            else
            {
                App.Current.Properties[username] = password;
                await App.Current.SavePropertiesAsync();
                return true;
            }
        }
    }
}
