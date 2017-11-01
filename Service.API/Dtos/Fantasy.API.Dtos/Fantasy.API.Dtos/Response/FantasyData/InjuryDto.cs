using System.Collections.Generic;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    class InjuryDto
    {
        public LeagueDto League { get; set; }
        public IEnumerable<InjuriesTeamDto> teams { get; set; }
        public string _comment { get; set; }
    }

    public class InjuriesTeamDto
    {

    }
}
