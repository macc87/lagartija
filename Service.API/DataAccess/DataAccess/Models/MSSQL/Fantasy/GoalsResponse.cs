﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    public class GoalsResponse
    {
        public List<Goal> Goals { get; set; }
        public string _comment { get; set; }
    }
}
