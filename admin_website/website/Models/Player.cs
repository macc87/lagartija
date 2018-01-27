using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Salary { get; set; }
    }
}