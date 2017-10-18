using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAccess.Models.DummyModel
{
    public class ContestViewModel
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
        public List<CheckBoxViewModel> Games { get; set; }

        public string SelectedContestType { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }
    }
}