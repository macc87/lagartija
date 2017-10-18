using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataAccess.Models.DummyModel
{
    public class GameViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Team HomeTeam { get; set; }
        [Required]
        public Team AwayTeam { get; set; }
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Scheduled { get; set; }
        [Required, DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        [Required]
        public Stadium Venue { get; set; }
        [Required]
        public double Temperture { get; set; }
        [Required]
        public double Humidity { get; set; }
        [Required]
        public ClimaConditions Wheather { get; set; }

        public string SelectedHomeTeam { get; set; }
        public string SelectedAwayTeam { get; set; }
        public string SelectedStadium { get; set; }
        public string SelectedWheather { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
        public IEnumerable<SelectListItem> Stadiums { get; set; }
        public IEnumerable<SelectListItem> ClimaConditions { get; set; }
    }

}