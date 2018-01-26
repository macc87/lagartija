using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class Promotion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PromotionContent { get; set; }
        [Required, Display(Name = "Initial Date"), DataType(DataType.Date)]
        public DateTime InitialDate { get; set; }
        [Required, Display(Name = "Final Date"), DataType(DataType.Date)]
        public DateTime FinalDate { get; set; }
    }
}