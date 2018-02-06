﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class ClimaConditions
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Condition { get; set; }
    }
}