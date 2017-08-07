﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class Sport
    {
        [Required]
        public int Id { get; set; }
        [Display(Name="Sport Name")]
        public string SportName { get; set; }
    }
}