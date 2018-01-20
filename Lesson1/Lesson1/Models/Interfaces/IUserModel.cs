using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Models.Interfaces
{
    // Just to resolve through DependencyService.
    public interface IUserModel
    {
        string Username { get; }

        bool IsLoggedIn { get; }
    }
}
