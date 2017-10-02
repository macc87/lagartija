using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website.Models
{
    public class Game
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Team HomeTeam { get; set; }
        [Required]
        public Team AwayTeam { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime Scheduled { get; set; }
        [Required, DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [Required]
        public Stadium Venue { get; set; }
        [Required]
        public double Temperture { get; set; }
        public double Humidity { get; set; }
        public ClimaConditions Wheather { get; set; }
    }
}