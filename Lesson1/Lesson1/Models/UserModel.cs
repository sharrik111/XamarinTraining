using Lesson1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Models
{
    /// <summary>
    /// Stores basic user information in runtime.
    /// </summary>
    /// <seealso cref="Lesson1.Models.Interfaces.IUserModel" />
    class UserModel : IUserModel
    {
        public UserModel(string username)
        {
            Username = username;
        }

        public string Username { get; protected set; }

        public bool IsLoggedIn => !string.IsNullOrEmpty(Username);
    }
}
