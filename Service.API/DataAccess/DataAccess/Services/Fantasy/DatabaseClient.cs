using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using System;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.DataAccess.Services.Fantasy.Core;
using System.Collections.Generic;

namespace Fantasy.API.DataAccess.Services.Fantasy
{
    public class DatabaseClient : IFantasyDataClient
    {
        private DatabaseCore _dbClientCore;
        private bool _disposed = false;

        public DatabaseClient()
        {
            _dbClientCore = new DatabaseCore();
        }

        #region [Dispose]

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_dbClientCore != null)
                {
                    _dbClientCore.Dispose();
                }
                _dbClientCore = null;
            }
            _disposed = true;
        }

        #endregion

        public async Task<ServiceResult<ContestsResponse>> GetContestsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetContestsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestsResponse>(exception);
            }
        }

        public async Task<ServiceResult<ContestsResponse>> GetActiveContestsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetContestsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestsResponse>(exception);
            }
        }

        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(Int64 teamId)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromTeamAsync(teamId);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }

        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamRefurbishedAsync(Int64 teamId)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromTeamAsync(teamId);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }

        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamsAsync(List<Models.MSSQL.Fantasy.Team> teams)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromTeamsAsync(teams);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }

        public async Task<ServiceResult<TeamResponse>> GetTeamAsync(Int64 teamId)
        {
            try
            {
                var result = await _dbClientCore.GetTeamAsync(teamId);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<TeamResponse>(exception);
            }
        }

        public async Task<ServiceResult<NotificationsResponse>> GetActiveNotificationsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetActiveNotificationsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NotificationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(Account user)
        {
            try
            {
                var result = await _dbClientCore.GetUserActiveNotificationsAsync(user);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NotificationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(string user)
        {
            try
            {
                var result = await _dbClientCore.GetUserActiveNotificationsAsync(user);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NotificationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<InformationsResponse>> GetInformationsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetInformationsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InformationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<InformationsResponse>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                var result = await _dbClientCore.GetInformationsAsync(start, end);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InformationsResponse>(exception);
            }
        }

        public async Task<ServiceResult<PromotionsResponse>> GetPromotionsAsync()
        {
            try
            {
                var result = await _dbClientCore.GetPromotionsAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PromotionsResponse>(exception);
            }
        }

        public async Task<ServiceResult<DateTime>> GetNextContestTimeAsync(IEnumerable<Contest> contests)
        {
            try
            {
                var result = await _dbClientCore.GetNextContestTimeAsync(contests);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<DateTime>(exception);
            }
        }

        public async Task<ServiceResult<List<ContestGame>>> GetContestGamesAsync(Int64 contest)
        {
            try
            {
                var result = await _dbClientCore.GetContestGamesAsync(contest);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<List<ContestGame>>(exception);
            }
        }

        public async Task<ServiceResult<ContestTypeResponse>> GetContestTypesAsync()
        {
            try
            {
                var result = await _dbClientCore.GetContesTypesAsync();

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestTypeResponse>(exception);
            }
        }

        public async Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type)
        {

            try
            {
                var result = await _dbClientCore.GetContestFilteredbyAsync(type);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestsResponse>(exception);
            }
        }

        public async Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(double smallEntry, double bigEntry)
        {
            try
            {
                var result = await _dbClientCore.GetContestFilteredbyAsync(smallEntry, bigEntry);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestsResponse>(exception);
            }
        }

        public async Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type, double smallEntry, double bigEntry)
        {
            try
            {
                var result = await _dbClientCore.GetContestFilteredbyAsync(type, smallEntry, bigEntry);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestsResponse>(exception);
            }
        }

        public async Task<ServiceResult<UserResponse>> GetUserInfoAsync(string login)
        {
            try
            {
                var result = await _dbClientCore.GetUserInfoAsync(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<UserResponse>(exception);
            }
        }

        public async Task<ServiceResult<List<Account>>> GetUsersBestRivalsAsync(string login)
        {
            try
            {
                var result = await _dbClientCore.GetUsersBestRivalsAsync(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<List<Account>> (exception);
            }
        }

        public async Task<ServiceResult<List<Account>>> GetUsersWorstRivalsAsync(string login)
        {
            try
            {
                var result = await _dbClientCore.GetUsersWorstRivalsAsync(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<List<Account>>(exception);
            }
        }

        public async Task<ServiceResult<List<Account>>> GetUserFriendsAsync(string login)
        {
            try
            {
                var result = await _dbClientCore.GetUserFriendsAsync(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<List<Account>>(exception);
            }
        }

        public async Task<ServiceResult<LineupsResponse>> GetLineupsAsync(string login)
        {
            try
            {
                var result = await _dbClientCore.GetLineupsAsync(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<LineupsResponse>(exception);
            }
        }
        public async Task<ServiceResult<LineupsResponse>> GetLineupsFromContestAsync(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetLineupsFromContestAsync(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<LineupsResponse>(exception);
            }
        }

        public async Task<ServiceResult<LineupsResponse>> GetActiveLineupsAsync(string login)
        {
            try
            {
                var result = await _dbClientCore.GetActiveLineupsAsync(login);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<LineupsResponse>(exception);
            }
        }

        public async Task<ServiceResult<ContestResponse>> GetContest(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetContest(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestResponse>(exception);
            }
        }
        public async Task<ServiceResult<ContestsResponse>> GetContests(DateTime startDate)
        {
            try
            {
                var result = await _dbClientCore.GetContests(startDate);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ContestsResponse>(exception);
            }
        }
        public async Task<ServiceResult<GamesResponse>> GetGamesFromContest(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetGamesFromContest(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<GamesResponse>(exception);
            }
        }
        public async Task<ServiceResult<TeamsResponse>> GetTeamsFromGames(List<Game> games)
        {
            try
            {
                var result = await _dbClientCore.GetTeamsFromGames(games);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<TeamsResponse>(exception);
            }
        }
        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeam(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromTeam(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }
        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeams(List<Team> teams)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromTeams(teams);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }
        public async Task<ServiceResult<GoalsResponse>> GetGoalsfromContest(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetGoalsfromContest(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<GoalsResponse>(exception);
            }
        }
        public async Task<ServiceResult<PlayerResponse>> GetPlayer(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetPlayer(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayerResponse>(exception);
            }
        }
        public async Task<ServiceResult<PlayersResponse>> GetPlayersFromLineup(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetPlayersFromLineup(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PlayersResponse>(exception);
            }
        }
        public async Task<ServiceResult<GamesResponse>> GetGamesfromTeam(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetGamesfromTeam(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<GamesResponse>(exception);
            }
        }
        public async Task<ServiceResult<GameResponse>> GetGame(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetGame(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<GameResponse>(exception);
            }
        }
        public async Task<ServiceResult<NewsResponse>> GetNews(DateTime start, DateTime end)
        {
            try
            {
                var result = await _dbClientCore.GetNews(start, end);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NewsResponse>(exception);
            }
        }
        public async Task<ServiceResult<NewsResponse>> GetPlayerNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                var result = await _dbClientCore.GetPlayerNews(id, start, end);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NewsResponse>(exception);
            }
        }
        public async Task<ServiceResult<NewsResponse>> GetTeamNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                var result = await _dbClientCore.GetTeamNews(id, start, end);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NewsResponse>(exception);
            }
        }
        public async Task<ServiceResult<LineupsResponse>> GetLineupsofContest(Int64 id)
        {
            try
            {
                var result = await _dbClientCore.GetLineupsofContest(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<LineupsResponse>(exception);
            }
        }
       
    }
}
