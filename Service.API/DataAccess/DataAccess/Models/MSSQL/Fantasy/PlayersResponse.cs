﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    public class PlayersResponse
    {
        public List<Player> Players { get; set; }
        public string _comment { get; set; }
    }
}