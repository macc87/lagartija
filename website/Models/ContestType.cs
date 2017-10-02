using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class ContestType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}