using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Dtos.Response.FantasyData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Service.Mapping.ResponseMapping
{
    public class DtoFactoryResponse
    {
        public async Task<InjuryDto> Create(InjuriesBO poco)
        {
            var result = new InjuryDto
            {
                _comment = poco._comment
            };
            result.League = await Create(poco.League);
            result.teams = await Create(poco.teams);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<InjuriesTeamDto>> Create(IEnumerable<InjuriesTeamBO> teams)
        {
            var result = new List<InjuriesTeamDto>();
            return await Task.FromResult(result);
        }

        public async Task<LeagueDto> Create(LeagueBO league)
        {
            var result = new LeagueDto
            {
                alias = league.alias,
                id = league.id,
                name = league.name
            };
            return await Task.FromResult(result);
        }
    }
}
