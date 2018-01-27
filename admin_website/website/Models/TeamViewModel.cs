using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace website.Models
{
    public class TeamViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
        [Required]
        public Sport Sport { get; set; }

        public string LogoPath { get; set; }
        public string SelectedSport { get; set; }
        public IEnumerable<SelectListItem> Sports { get; set; }
    }
}