using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class LineUp
    {
        [Required]
        public int Id { get; set; }
        //[Required]
        //public FantasyUser User { get; set; }
        [Required]
        public List<Player> Players { get; set; }
        public List<Contest> Contests { get; set; }


    }
}