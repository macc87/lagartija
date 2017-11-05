using Fantasy.API.DataAccess.Models.Services.FantasyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class InjuriesBO
    {
        public LeagueBO League { get; set; }
        public IEnumerable<InjuriesTeamBO> teams { get; set; }
        public string _comment { get; set; }
    }

    public class InjuriesTeamBO
    {

    }
}
