using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.DummyModel
{
    public class News
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name = "News Title")]
        public string NewsTitle { get; set; }
        [Required, Display(Name = "News Content")]
        public string NewsContent { get; set; }
    }
}