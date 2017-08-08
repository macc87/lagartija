using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class Notification
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name="Notification Name")]
        public string Name { get; set; }
        [Required, Display(Name = "Notification Content")]
        public string Content { get; set; }
    }
}