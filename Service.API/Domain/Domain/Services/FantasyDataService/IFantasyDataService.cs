using System;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using System.Collections.Generic;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public interface IFantasyDataService : IDisposable
    {
        Task<ServiceResult<List<ContestBO>>> GetContestsAsync();
        Task<ServiceResult<TeamBO>> GetTeamAsync(int teamId);
        Task<ServiceResult<List<ContestBO>>> GetActiveContestsAsync();
        Task<ServiceResult<List<NotificationBO>>> GetActiveNotificationsAsync();
        Task<ServiceResult<List<NotificationBO>>> GetUserActiveNotificationsAsync(string login);
        Task<ServiceResult<List<InformationBO>>> GetInformationsAsync();
        Task<ServiceResult<List<InformationBO>>> GetInformationsAsync(DateTime start, DateTime end);
        Task<ServiceResult<List<PromotionBO>>> GetPromotionsAsync();
        Task<ServiceResult<DateTimeBO>> GetNextContestTimeAsync(List<DataAccess.Models.MSSQL.Fantasy.Contest> contests);
        Task<ServiceResult<ContestGamesBO>> GetContestGamesAsync(Int64 id);
        Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(Int64 typeID);
        Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(double smallEntry, double bigEntry);
        Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(Int64 typeID, double smallEntry, double bigEntry);
        Task<ServiceResult<UserBO>> GetUserInfoAsync(string login);
        Task<ServiceResult<List<UserBO>>> GetUsersBestRivalsAsync(string login);
        Task<ServiceResult<List<UserBO>>> GetUsersWorstRivalsAsync(string login);
        Task<ServiceResult<List<UserBO>>> GetUserFriendsAsync(string login);
        Task<ServiceResult<List<LineupBO>>> GetLineupsAsync(string login);
        Task<ServiceResult<List<LineupBO>>> GetLineupsFromcontestAsync(Int64 id);
        Task<ServiceResult<List<LineupBO>>> GetActiveLineupsAsync(string login);
        Task<ServiceResult<ContestBO>> GetContestAsync(Int64 id);
        Task<ServiceResult<List<ContestBO>>> GetContestsAsync(DateTime startDate);
        Task<ServiceResult<List<GameBO>>> GetGamesFromContestAsync(Int64 id);
        Task<ServiceResult<List<TeamBO>>> GetTeamsFromGames(List<Game> games);
        Task<ServiceResult<GoalsBO>> GetGoalsfromContest(Int64 id);
        Task<ServiceResult<PlayerBO>> GetPlayer(Int64 id);
        Task<ServiceResult<List<PlayerBO>>> GetPlayersFromLineup(Int64 id);
        Task<ServiceResult<List<GameBO>>> GetGamesfromTeam(Int64 id);
        Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamAsync(Int64 teamId);
        Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamsAsync(List<Team> teams);
        Task<ServiceResult<GameBO>> GetGameAsync(Int64 id);
        Task<ServiceResult<List<NewsBO>>> GetNews(DateTime start, DateTime end);
        Task<ServiceResult<List<NewsBO>>> GetPlayerNews(Int64 id, DateTime start, DateTime end);
        Task<ServiceResult<List<NewsBO>>> GetTeamNews(Int64 id, DateTime start, DateTime end);
    }
}
