using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Mapping.FantasyData
{
    class FantasyDataMapping
    {
        public async Task<ClimaConditionsBO> Create(ClimaConditions clima)
        {
            var result = new ClimaConditionsBO
            {
                ClimaId = clima.ClimaId,
                Condition = clima.Condition
            };
            return await Task.FromResult(result);
        }
        public async Task<ContestTypeBO> Create(ContestType ctype)
        {
            var result = new ContestTypeBO
            {
                ContestTypeId = ctype.ContestTypeId,
                Type = ctype.Type
            };
            return await Task.FromResult(result);
        }
        public async Task<PromotionBO> Create(Promotion promo)
        {
            var result = new PromotionBO
            {
                PromoId = promo.PromoId,
                Code = promo.Code,
                Content = promo.Content,
                Name = promo.Name
            };
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<InjuriesTeamBO>> Create(TeamInjury[] teams)
        {
            var result = new List<InjuriesTeamBO>();
            return await Task.FromResult(result);
        }
    }
}
