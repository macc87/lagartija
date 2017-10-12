using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class Stadium
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name="Stadium Name")]
        public String Name { get; set; }
    }
}