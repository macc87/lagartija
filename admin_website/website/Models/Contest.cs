using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class Contest
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name = "Contest Name")]
        public string Name { get; set; }
        [Required, Display(Name = "Signed Up Contestants")]
        public int SignedUp { get; set; }
        [Required, Display(Name = "Max Capacity")]
        public int MaxCapacity { get; set; }
        [Required, Display(Name = "Entry Fee")]
        public double EntryFee { get; set; }
        [Required, Display(Name = "Salary Cap")]
        public double Cap { get; set; }
        [Required, Display(Name = "Type of the Contest")]
        public ContestType ContestType { get; set; }
        [Required]
        public List<Game> Games { get; set; }

        public DateTime FirstGame {
            get {
                return Games.Min(t => t.Scheduled);
            }
        }
    }
}



/*
•	Games(1,*) (Ya dicen el deporte todos los Games tienen que ser del mismo deporte)
 */
