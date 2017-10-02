using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace website.Models
{
    public class ContestToGame
    {
        public int Id { get; set; }
        public int ContestId { get; set; }
        public int GameId { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual Game Game { get; set; }
    }

}