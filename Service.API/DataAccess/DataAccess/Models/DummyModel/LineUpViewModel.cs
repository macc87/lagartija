using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAccess.Models.DummyModel
{
    public class LineUpViewModel
    {
        public int Id { get; set; }
        public FantasyUser User { get; set; }

        public List<CheckBoxViewModel> Players { get; set; }
        public List<CheckBoxViewModel> Contests { get; set; }

        public List<string> ContestNames { get; set; }

    }
}