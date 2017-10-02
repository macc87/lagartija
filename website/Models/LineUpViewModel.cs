using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace website.Models
{
    public class LineUpViewModel
    {
        public int Id { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public FantasyUser User { get; set; }
        public string SelectedUser { get; set; }

        public List<CheckBoxViewModel> Players { get; set; }
        public List<CheckBoxViewModel> Contests { get; set; }

    }
}