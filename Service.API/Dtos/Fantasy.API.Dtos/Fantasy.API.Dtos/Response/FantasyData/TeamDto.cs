using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    class TeamDto
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string TeamLogo { get; set; }

        public SportDto Sport { get; set; }

        public IEnumerable<PlayerDto> Players { get; set; }
    }
}
