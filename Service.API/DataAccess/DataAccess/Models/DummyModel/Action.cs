using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.DummyModel
{
    public class Action
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name = "Action Name")]
        public string ActionName { get; set; }
    }
}