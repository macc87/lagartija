using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAccess.Models.DummyModel
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public Sport Sport { get; set; }

        public string SelectedSport { get; set; }
        public IEnumerable<SelectListItem> Sports { get; set; }
    }
}