using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class TeamDto
    {
        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public string TeamLogo { get; set; }

        public SportDto Sport { get; set; }

        public string Abbr { get; set; }

        public string Market { get; set; }

        public IEnumerable<LeagueDto> Leagues { get; set; }

        public IEnumerable<PlayerDto> Players { get; set; }
        public string _comment { get; set; }
    }
}
