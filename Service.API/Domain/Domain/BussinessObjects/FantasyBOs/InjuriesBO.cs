using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BussinessObjects.FantasyBOs
{
    public class InjuriesBO
    {
        public InjuriesLeagueBO League { get; set; }
        public InjuriesTeamBO[] teams { get; set; }
        public string _comment { get; set; }
    }

    public class InjuriesLeagueBO
    { }

    public class InjuriesTeamBO
    {

    }
}
