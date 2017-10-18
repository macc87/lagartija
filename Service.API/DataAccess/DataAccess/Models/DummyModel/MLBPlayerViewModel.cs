using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAccess.Models.DummyModel
{
    public class MLBPlayerViewModel
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public Team Team { get; set; }
        public Position Position { get; set; }

        public string SportPositionError { get; set; }


        public string PhotoPath { get; set; }

        public string SelectedTeam { get; set; }
        public string SelectedPosition { get; set; }
        public string SelectedPlayer { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }
        public IEnumerable<SelectListItem> Players { get; set; }
    }
}