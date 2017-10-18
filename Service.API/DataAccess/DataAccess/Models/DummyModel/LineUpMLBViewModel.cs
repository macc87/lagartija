﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAccess.Models.DummyModel
{
    public class LineUpMLBViewModel
    {
        public LineUp Lineup { get; set; }

        public List<MLBPlayer> Players { get; set; }
        public List<Contest> Contests { get; set; }

    }
}