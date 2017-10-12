using DataAccess.Models.Services.FantasyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ServicesHandler.Core;

namespace DataAccess.Services.Fantasy.Interfase
{
    public interface IFantasyClient : IDisposable
    {
        Task<ServiceResult<InjuryResponse>> GetInjuriesAsync();
        Task<ServiceResult<ScheduleBase>> GetDailyScheduleAsync(DateTime date);
        Task<ServiceResult<GameSummaryResponse>> GetGameSummaryAsync(string gameId);
        ////Task<ServiceResult<GameBoxScore>> GetGameBoxScoreAsync(string gameId);
    }
}
