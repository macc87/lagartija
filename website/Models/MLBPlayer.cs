using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class MLBPlayer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Player Player { get; set; }
        [Required]
        public Team Team { get; set; }
        [Required]
        public Position Position { get; set; }
    }
}