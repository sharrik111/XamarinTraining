using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Models.DB
{
    [Table(nameof(User))]
    class User
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
