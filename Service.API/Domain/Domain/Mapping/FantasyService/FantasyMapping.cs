using Fantasy.API.DataAccess.Models.Services.FantasyData;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Mapping.FantasyService
{
    internal class FantasyMapping
    {
        public async Task<InjuriesBO> Create(InjuryResponse poco)
        {
            var result = new InjuriesBO
            {
                _comment = poco._comment
            };
            result.League = await Create(poco.league);
            result.teams = await Create(poco.teams);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<InjuriesTeamBO>> Create(TeamInjury[] teams)
        {
            var result = new List<InjuriesTeamBO>();
            return await Task.FromResult(result);
        }

        public async Task<LeagueBO> Create(League league)
        {
            var result = new LeagueBO
            {
                alias = league.alias,
                id = league.id,
                name = league.name
            };
            return await Task.FromResult(result);
        }
    }
}
