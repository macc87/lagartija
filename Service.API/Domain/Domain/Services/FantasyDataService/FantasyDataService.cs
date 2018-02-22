using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Domain.Services.FantasyDataService.Core;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public class FantasyDataService : IFantasyDataService
    {
        private bool _disposed;
        private readonly FantasyDataServiceCore _fantasyDataServiceCore;

        public FantasyDataService(IFantasyDataClient client)
        {
            Check.NotNull(client, "client");
            _fantasyDataServiceCore = new FantasyDataServiceCore(client);
        }

        public async Task<ServiceResult<List<ContestBO>>> GetContestsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);

            }
        }
        public async Task<ServiceResult<List<ContestBO>>> GetActiveContestsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetActiveContestsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);

            }
        }
        public async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamAsync(Int64 teamId)
        {
            try
            {
                return await _fantasyDataServiceCore.GetPlayersFromTeamAsync(teamId);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<PlayerBO>>(exception);
            }
        }
        public async Task<ServiceResult<TeamBO>> GetTeamAsync(int teamId)
        {
            try
            {
                return await _fantasyDataServiceCore.GetTeamAsync(teamId);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<TeamBO>(exception);
            }
        }
        public async Task<ServiceResult<List<NotificationBO>>> GetActiveNotificationsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetActiveNotificationsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NotificationBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<NotificationBO>>> GetUserActiveNotificationsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetUserActiveNotificationsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NotificationBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<InformationBO>>> GetInformationsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetInformationsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<InformationBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<InformationBO>>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                return await _fantasyDataServiceCore.GetInformationsAsync(start, end);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<InformationBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<PromotionBO>>> GetPromotionsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetPromotionsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<PromotionBO>>(exception);
            }
        }
        public async Task<ServiceResult<DateTimeBO>> GetNextContestTimeAsync(List<Contest> contests)
        {
            try
            {
                return await _fantasyDataServiceCore.GetNextContestTime(contests);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<DateTimeBO>(exception);
            }
        }
        public async Task<ServiceResult<ContestGamesBO>> GetContestGamesAsync(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestGamesAsync(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<ContestGamesBO>(exception);
            }
        }
        public async Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(Int64 typeID)
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestFilteredbyAsync(typeID);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(double smallEntry, double bigEntry)
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestFilteredbyAsync(smallEntry, bigEntry);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(Int64 typeID, double smallEntry, double bigEntry)
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestFilteredbyAsync(typeID, smallEntry, bigEntry);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        public async Task<ServiceResult<UserBO>> GetUserInfoAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetUserInfoAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<UserBO>(exception);
            }
        }
        public async Task<ServiceResult<List<UserBO>>> GetUsersBestRivalsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetUsersBestRivalsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<UserBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<UserBO>>> GetUsersWorstRivalsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetUsersWorstRivalsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<UserBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<UserBO>>> GetUserFriendsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetUserFriendsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<UserBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<LineupBO>>> GetLineupsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetLineupsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<LineupBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<LineupBO>>> GetLineupsFromcontestAsync(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetLineupsFromcontestAsync(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<LineupBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<LineupBO>>> GetActiveLineupsAsync(string login)
        {
            try
            {
                return await _fantasyDataServiceCore.GetLineupsAsync(login);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<LineupBO>>(exception);
            }
        }
        public async Task<ServiceResult<ContestBO>> GetContestAsync(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetContest(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<ContestBO>(exception);
            }
        }
        public async Task<ServiceResult<List<ContestBO>>> GetContestsAsync(DateTime startDate)
        {
            try
            {
                return await _fantasyDataServiceCore.GetContests(startDate);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<GameBO>>> GetGamesFromContestAsync(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetGamesFromContest(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<GameBO>>(exception);
            }
        }
        public async Task<ServiceResult<GameBO>> GetGameAsync(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetGameAsync(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<GameBO>(exception);
            }
        }
        public async Task<ServiceResult<List<TeamBO>>> GetTeamsFromGames(List<Game> games)
        {
            try
            {
                return await _fantasyDataServiceCore.GetTeamsFromGames(games);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<TeamBO>>(exception);
            }
        }
        public async Task<ServiceResult<GoalsBO>> GetGoalsfromContest(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetGoalsfromContest(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<GoalsBO>(exception);
            }
        }
        public async Task<ServiceResult<PlayerBO>> GetPlayer(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetPlayer(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<PlayerBO>(exception);
            }
        }
        public async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromLineup(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetPlayersFromLineup(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<PlayerBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<GameBO>>> GetGamesfromTeam(Int64 id)
        {
            try
            {
                return await _fantasyDataServiceCore.GetGamesfromTeam(id);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<GameBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamsAsync(List<Team> teams)
        {
            try
            {
                return await _fantasyDataServiceCore.GetPlayersFromTeamsAsync(teams);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<PlayerBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<NewsBO>>> GetNews(DateTime start, DateTime end)
        {
            try
            {
                return await _fantasyDataServiceCore.GetNews(start, end);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NewsBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<NewsBO>>> GetPlayerNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                return await _fantasyDataServiceCore.GetPlayerNews(id, start, end);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NewsBO>>(exception);
            }
        }
        public async Task<ServiceResult<List<NewsBO>>> GetTeamNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                return await _fantasyDataServiceCore.GetTeamNews(id, start, end);
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<NewsBO>>(exception);
            }
        }
        #region [Disposing]

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fantasyDataServiceCore.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyDataService()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
