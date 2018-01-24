using Fantasy.API.DataAccess.Models.Services.FantasyData;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;

namespace Fantasy.API.DataAccess.Services.Fantasy.Interfase
{
    public interface IFantasyDataClient : IDisposable
    {
        Task<ServiceResult<ContestsResponse>> GetContestsAsync();
        Task<ServiceResult<ContestsResponse>> GetActiveContestsAsync();
        Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(int teamId);
        Task<ServiceResult<TeamResponse>> GetTeamAsync(int teamId);
        Task<ServiceResult<NotificationsResponse>> GetActiveNotificationsAsync();
        Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(Account user);
        Task<ServiceResult<InformationsResponse>> GetInformationsAsync();
        Task<ServiceResult<InformationsResponse>> GetInformationsAsync(DateTime start, DateTime end);
        Task<ServiceResult<PromotionsResponse>> GetPromotionsAsync();
        Task<ServiceResult<DateTime>> GetNextContestTimeAsync(IEnumerable<Contest> contests);
        Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type);
        Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(double smallEntry, double bigEntry);
        Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type, double smallEntry, double bigEntry);
        Task<ServiceResult<ContestTypeResponse>> GetContestTypesAsync();
        //TODO: Task<ServiceResult<ContestResponse>> GetContestFilteredby(Prizing Rule);
        Task<ServiceResult<UserResponse>> GetUserInfoAsync(string login);
        Task<ServiceResult<List<Account>>> GetUsersBestRivalsAsync(string login);
        Task<ServiceResult<List<Account>>> GetUsersWorstRivalsAsync(string login);
        Task<ServiceResult<List<Account>>> GetUserFriendsAsync(string login);
        Task<ServiceResult<LineupsResponse>> GetLineupsAsync(string login);
        Task<ServiceResult<LineupsResponse>> GetActiveLineupsAsync(string login);
        Task<ServiceResult<ContestResponse>> GetContest(Int64 id);
        Task<ServiceResult<ContestsResponse>> GetContests(DateTime startDate);
        Task<ServiceResult<GamesResponse>> GetGamesFromContest(Int64 id);
        Task<ServiceResult<TeamsResponse>> GetTeamsFromGames(List<Models.MSSQL.Fantasy.Game> games);
        Task<ServiceResult<GoalsResponse>> GetGoalsfromContest(Int64 id);
        Task<ServiceResult<PlayerResponse>> GetPlayer(Int64 id);
        Task<ServiceResult<PlayersResponse>> GetPlayersFromLineup(Int64 id);
    }
};

        
