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

        #region GET Section

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

        public async Task<ServiceResult<ContestTypesResponse>> GetContestTypesAsync()
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
                return _dbClientCore.ExceptionHandler<ContestTypesResponse>(exception);
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
        public async Task<ServiceResult<TeamsResponse>> GetTeamsFromGame(long gameID)
        {
            try
            {
                var result = await _dbClientCore.GetTeamsFromGame(gameID);

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
        public async Task<ServiceResult<SportResponse>> GetSportAsync(long id)
        {
            try
            {
                var result = await _dbClientCore.GetSport(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<SportResponse>(exception);
            }
        }
        public async Task<ServiceResult<PositionResponse>> GetPositionAsync(long id)
        {
            try
            {
                var result = await _dbClientCore.GetPositionAsync(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PositionResponse>(exception);
            }
        }
        public async Task<ServiceResult<VenueResponse>> GetVenueAsync(long id)
        {
            try
            {
                var result = await _dbClientCore.GetVenueAsync(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<VenueResponse>(exception);
            }
        }
        public async Task<ServiceResult<ContestTypeResponse>> GetContestTypeAsync(long id)
        {
            try
            {
                var result = await _dbClientCore.GetContestTypeAsync(id);

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
        public async Task<ServiceResult<ClimaConditionResponse>> GetClimaConditionAsync(long id)
        {
            try
            {
                var result = await _dbClientCore.GetClimaConditionAsync(id);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ClimaConditionResponse>(exception);
            }
        }
        #endregion

        #region POST Section

        public async Task<ServiceResult<InformationResponse>> PostInformationAsync(Information info)
        {
            try
            {
                var result = await _dbClientCore.PostInformationAsync(info);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InformationResponse>(exception);
            }
        }
        public async Task<ServiceResult<PromotionResponse>> PostPromotionAsync(Promotion promo)
        {
            try
            {
                var result = await _dbClientCore.PostPromotionAsync(promo);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PromotionResponse>(exception);
            }
        }
        public async Task<ServiceResult<ContestTypeResponse>> PostContestTypeAsync(ContestType ctype)
        {
            try
            {
                var result = await _dbClientCore.PostContestTypeAsync(ctype);

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
        public async Task<ServiceResult<SportResponse>> PostSportAsync(Sport sport)
        {
            try
            {
                var result = await _dbClientCore.PostSportAsync(sport);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<SportResponse>(exception);
            }
        }
        public async Task<ServiceResult<PositionResponse>> PostPositionAsync(Position position)
        {
            try
            {
                var result = await _dbClientCore.PostPositionAsync(position);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PositionResponse>(exception);
            }
        }
        public async Task<ServiceResult<ClimaConditionResponse>> PostClimaConditionAsync(ClimaConditions ccond)
        {
            try
            {
                var result = await _dbClientCore.PostClimaConditionAsync(ccond);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ClimaConditionResponse>(exception);
            }
        }
        public async Task<ServiceResult<VenueResponse>> PostVenueAsync(Models.MSSQL.Fantasy.Venue venue)
        {
            try
            {
                var result = await _dbClientCore.PostVenueAsync(venue);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<VenueResponse>(exception);
            }
        }
        public async Task<ServiceResult<GoalResponse>> PostGoalAsync(Goal goal)
        {
            try
            {
                var result = await _dbClientCore.PostGoalAsync(goal);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<GoalResponse>(exception);
            }
        }
        public async Task<ServiceResult<NotificationResponse>> PostNotificationAsync(Notification notification)
        {
            try
            {
                var result = await _dbClientCore.PostNotificationAsync(notification);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<NotificationResponse>(exception);
            }
        }
        public async Task<ServiceResult<TeamResponse>> PostTeamAsync(Team team)
        {
            try
            {
                var result = await _dbClientCore.PostTeamAsync(team);

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
        public async Task<ServiceResult<SingleNewsResponse>> PostNewsAsync(News News)
        {
            try
            {
                var result = await _dbClientCore.PostNewsAsync(News);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<SingleNewsResponse>(exception);
            }
        }
        public async Task<ServiceResult<LeagueResponse>> PostLeagueAsync(Models.MSSQL.Fantasy.League league)
        {
            try
            {
                var result = await _dbClientCore.PostLeagueAsync(league);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<LeagueResponse>(exception);
            }
        }
        public async Task<ServiceResult<Models.MSSQL.Fantasy.InjuryResponse>> PostInjuryAsync(Models.MSSQL.Fantasy.Injury injury)
        {
            try
            {
                var result = await _dbClientCore.PostInjuryAsync(injury);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InjuryResponse>(exception);
            }
        }
        public async Task<ServiceResult<LineupResponse>> PostLineupAsync(LineUp lineups)
        {
            try
            {
                var result = await _dbClientCore.PostLineupAsync(lineups);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<LineupResponse>(exception);
            }
        }
        public async Task<ServiceResult<UserResponse>> PostAccountAsync(Account user)
        {
            try
            {
                var result = await _dbClientCore.PostAccountAsync(user);

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
        public async Task<ServiceResult<PlayerResponse>> PostPlayerAsync(Models.MSSQL.Fantasy.Player player)
        {
            try
            {
                var result = await _dbClientCore.PostPlayerAsync(player);

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
        public async Task<ServiceResult<GameResponse>> PostGameAsync(Models.MSSQL.Fantasy.Game game)
        {
            try
            {
                var result = await _dbClientCore.PostGameAsync(game);

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
        public async Task<ServiceResult<ContestResponse>> PostContestAsync(Contest contest)
        {
            try
            {
                var result = await _dbClientCore.PostContestAsync(contest);

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
        #endregion

        #region PUT Section

        public async Task<ServiceResult<InformationResponse>> PutInformationAsync(Information info)
        {
            try
            {
                var result = await _dbClientCore.PutInformationAsync(info);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<InformationResponse>(exception);
            }
        }
        public async Task<ServiceResult<PromotionResponse>> PutPromotionAsync(Promotion promo)
        {
            try
            {
                var result = await _dbClientCore.PutPromotionAsync(promo);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PromotionResponse>(exception);
            }
        }
        public async Task<ServiceResult<ContestTypeResponse>> PutContestTypeAsync(ContestType ctype)
        {
            try
            {
                var result = await _dbClientCore.PutContestTypeAsync(ctype);

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
        public async Task<ServiceResult<SportResponse>> PutSportAsync(Sport sport)
        {
            try
            {
                var result = await _dbClientCore.PutSportAsync(sport);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<SportResponse>(exception);
            }
        }
        public async Task<ServiceResult<PositionResponse>> PutPositionAsync(Position position)
        {
            try
            {
                var result = await _dbClientCore.PutPositionAsync(position);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<PositionResponse>(exception);
            }
        }
        public async Task<ServiceResult<ClimaConditionResponse>> PutClimaConditionAsync(ClimaConditions ccond)
        {
            try
            {
                var result = await _dbClientCore.PutClimaConditionAsync(ccond);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<ClimaConditionResponse>(exception);
            }
        }
        public async Task<ServiceResult<VenueResponse>> PutVenueAsync(Models.MSSQL.Fantasy.Venue venue)
        {
            try
            {
                var result = await _dbClientCore.PutVenueAsync(venue);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<VenueResponse>(exception);
            }
        }
        public async Task<ServiceResult<GoalResponse>> PutGoalAsync(Goal goal)
        {
            try
            {
                var result = await _dbClientCore.PutGoalAsync(goal);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<GoalResponse>(exception);
            }
        }
        #endregion

        #region Delete Section

        public async Task<ServiceResult<bool>> DeleteInformationAsync(Information info)
        {
            try
            {
                var result = await _dbClientCore.DeleteInformationAsync(info);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<bool>(exception);
            }
        }
        public async Task<ServiceResult<bool>> DeletePromotionAsync(Promotion promo)
        {
            try
            {
                var result = await _dbClientCore.DeletePromotionAsync(promo);

                if (result.HasError)
                    throw new ServiceException(result.InnerException, httpStatusCode: result.HttpStatusCode,
                        message: result.Messages.Description, serviceResultCodeMessage: result.Messages.Code);

                return result;
            }
            catch (Exception exception)
            {
                return _dbClientCore.ExceptionHandler<bool>(exception);
            }
        }
        
        #endregion
    }
}
