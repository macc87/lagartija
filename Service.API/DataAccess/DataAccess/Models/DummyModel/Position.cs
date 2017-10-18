using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.DummyModel
{
    public class Position
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name ="Position Name")]
        public string PositionName { get; set; }
        [Required]
        public Sport Sport { get; set; }
    }
}