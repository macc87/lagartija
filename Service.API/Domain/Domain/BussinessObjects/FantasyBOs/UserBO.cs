﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class UserBO
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public long Money { get; set; }

        public long Point { get; set; }
    }
}
