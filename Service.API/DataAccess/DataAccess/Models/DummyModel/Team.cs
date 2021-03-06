﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.DummyModel
{
    public class Team
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
        [Required]
        public Sport Sport { get; set; }
    }
}