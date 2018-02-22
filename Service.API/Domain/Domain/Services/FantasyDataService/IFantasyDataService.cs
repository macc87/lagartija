using System;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using System.Collections.Generic;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public interface IFantasyDataService : IDisposable
    {
        Task<ServiceResult<List<ContestBO>>> GetContestsAsync();
        Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamAsync(int teamId);
        Task<ServiceResult<TeamBO>> GetTeamAsync(int teamId);
        Task<ServiceResult<List<ContestBO>>> GetActiveContestsAsync();
        Task<ServiceResult<List<NotificationBO>>> GetActiveNotificationsAsync();
        Task<ServiceResult<List<NotificationBO>>> GetUserActiveNotificationsAsync(string login);
        Task<ServiceResult<List<InformationBO>>> GetInformationsAsync();
        Task<ServiceResult<List<InformationBO>>> GetInformationsAsync(DateTime start, DateTime end);
        Task<ServiceResult<List<PromotionBO>>> GetPromotionsAsync();
        Task<ServiceResult<DateTimeBO>> NextContestTime(List<API.DataAccess.Models.MSSQL.Fantasy.Contest> contests);
    }
}
