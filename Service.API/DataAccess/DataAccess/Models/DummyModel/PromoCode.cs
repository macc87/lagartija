using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class PromoCode
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name="Promotion Name")]
        public string Name { get; set; }
        [Required, Display(Name = "Promotion Code")]
        public string Code { get; set; }
    }
}