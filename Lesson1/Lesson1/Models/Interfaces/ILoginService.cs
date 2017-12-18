using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Models.Interfaces
{
   // TODO: code review
   // write unit tests for service
    public interface ILoginService
    {
        bool TryToLogin(string username, string password);
    }
}
