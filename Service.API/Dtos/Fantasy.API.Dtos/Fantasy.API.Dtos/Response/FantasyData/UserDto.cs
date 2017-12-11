using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class UserDto
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
