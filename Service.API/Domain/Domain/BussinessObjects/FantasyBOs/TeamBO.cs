using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class TeamBO
    {
        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public string TeamLogo { get; set; }

        public SportBO Sport { get; set; }

        public string Abbr { get; set; }

        public string Market { get; set; }

        public List<PlayerBO> Players { get; set; }
        
        public List<LeagueBO> Leagues { get; set; }
    }
}
