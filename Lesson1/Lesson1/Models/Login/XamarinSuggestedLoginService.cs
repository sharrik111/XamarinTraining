using Lesson1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace Lesson1.Models.Login
{
    /// <summary>
    /// Login service which uses <see cref="Xamarin.Auth.AccountStore"/> staff.
    /// </summary>
    /// <seealso cref="Lesson1.Models.Interfaces.ILoginService" />
    class XamarinSuggestedLoginService : ILoginService
    {
        const string ServiceId = nameof(XamarinSuggestedLoginService);
        // AccountStore store = AccountStore.Create();

        public async Task<bool> TryToLoginAsync(string username, string password)
        {
            // AccountStore store = AccountStore.Create();
            Account account = (await AccountStore.Create().FindAccountsForServiceAsync(ServiceId)).FirstOrDefault(acc => acc.Username == username);
            if (account != null)
                return account.Properties["password"] == password;
            else
            {
                account = new Account(username);
                account.Properties["password"] = password;
                await AccountStore.Create().SaveAsync(account, ServiceId);
                return true;
            }
        }
    }
}
