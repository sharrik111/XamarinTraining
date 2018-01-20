using Lesson1.Models.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lesson1.Models.Login
{
    class SQLiteLoginService : ILoginService
    {
        // It would be better to create a separate application db managing class but I'm too lazy. It's just to check how it works.
        SQLiteAsyncConnection database =
            new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>(DependencyFetchTarget.NewInstance).GetLocalFilePath("Lesson1.db"));

        public async Task<bool> TryToLoginAsync(string username, string password)
        {
            if(!TableExists(database.GetConnection()))
                await database.CreateTableAsync<DB.User>();

            var user = await database.Table<DB.User>().Where(u => u.Username == username).FirstOrDefaultAsync();
            if(user == null)
            {
                await database.InsertAsync(new DB.User { Username = username, Password = password });
                return true;
            }
            else
            {
                return user.Password == password;
            }
        }

        private bool TableExists(SQLiteConnection connection)
        {
            SQLiteCommand cmd = connection.CreateCommand($"SELECT * FROM sqlite_master WHERE type = 'table' AND name = '{nameof(DB.User)}'");
            return (cmd.ExecuteScalar<DB.User>() != null);
        }
    }
}
