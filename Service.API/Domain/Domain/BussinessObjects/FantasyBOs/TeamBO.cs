using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    class TeamBO
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string TeamLogo { get; set; }

        public SportBO Sport { get; set; }

        public IEnumerable<PlayerBO> Players { get; set; }
    }
}
