using Assurant.ASP.Api.Domain.Mapping;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyDataService.Core
{
    public class FantasyDataServiceCore : BaseService, IDisposable
    {
        private bool _disposed;
        private readonly IFantasyDataClient FantasyClient;

        public FantasyDataServiceCore(IFantasyDataClient fantasyClient)
        {
            FantasyClient = fantasyClient;
        }

        internal async Task<ServiceResult<List<ContestBO>>> GetContestsAsync()
        {
            try
            {
                var result = await FantasyClient.GetContestsAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get contests");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<ContestBO>>> GetActiveContestsAsync()
        {
            try
            {
                var result = await FantasyClient.GetActiveContestsAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get contests");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<NotificationBO>>> GetActiveNotificationsAsync()
        {
            try
            {
                var result = await FantasyClient.GetActiveNotificationsAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get notifications");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Notifications);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<NotificationBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamAsync(Int64 teamId)
        {
            try
            {
                var result = await FantasyClient.GetPlayersFromTeamAsync(teamId);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get players from team " + teamId);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Players);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<PlayerBO>>(exception);
            }
        }
        internal async Task<ServiceResult<TeamBO>> GetTeamAsync(int teamId)
        {
            try
            {
                var result = await FantasyClient.GetTeamAsync(teamId);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get team "+ teamId.ToString());
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Team);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<TeamBO>(exception);
            }

        }
        internal async Task<ServiceResult<List<NotificationBO>>> GetUserActiveNotificationsAsync(string userLogin)
        {
            try
            {
                var result = await FantasyClient.GetUserActiveNotificationsAsync(userLogin);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get notifications");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Notifications);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<NotificationBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<InformationBO>>> GetInformationsAsync()
        {
            try
            {
                var result = await FantasyClient.GetInformationsAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get notifications");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Informations);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<InformationBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<InformationBO>>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                var result = await FantasyClient.GetInformationsAsync(start, end);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get notifications");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Informations);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<InformationBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<PromotionBO>>> GetPromotionsAsync()
        {
            try
            {
                var result = await FantasyClient.GetPromotionsAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get promotions");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Promotions);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<PromotionBO>>(exception);
            }
        }
        internal async Task<ServiceResult<DateTimeBO>> GetNextContestTime(List<API.DataAccess.Models.MSSQL.Fantasy.Contest> contest)
        {
            try
            {
                var result = await FantasyClient.GetNextContestTimeAsync(contest);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get next contest time");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<DateTimeBO>(exception);
            }
        }

        internal async Task<ServiceResult<ContestGamesBO>> GetContestGamesAsync(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetContestGamesAsync(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get next contest games");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<ContestGamesBO>(exception);
            }
        }

        internal async Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(Int64 typeID)
        {
            try
            {
                var Ctypes = await FantasyClient.GetContestTypesAsync();
                ContestType ct = Ctypes.Result.Types.Find(x => x.ContestTypeId == typeID);
                var result = await FantasyClient.GetContestFilteredbyAsync(ct);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get next contest games");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }

        internal async Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(Int64 typeID, double smallEntry, double bigEntry)
        {
            try
            {
                var Ctypes = await FantasyClient.GetContestTypesAsync();
                ContestType ct = Ctypes.Result.Types.Find(x => x.ContestTypeId == typeID);
                var result = await FantasyClient.GetContestFilteredbyAsync(ct, smallEntry, bigEntry);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get next contest games");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<ContestBO>>> GetContestFilteredbyAsync(double smallEntry, double bigEntry)
        {
            try
            {
                var result = await FantasyClient.GetContestFilteredbyAsync(smallEntry, bigEntry);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get next contest games");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        internal async Task<ServiceResult<UserBO>> GetUserInfoAsync(string login)
        {
            try
            {
                var result = await FantasyClient.GetUserInfoAsync(login);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get user info");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.User);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<UserBO>(exception);
            }
        }

        internal async Task<ServiceResult<List<UserBO>>> GetUsersBestRivalsAsync(string login)
        {
            try
            {
                var result = await FantasyClient.GetUsersBestRivalsAsync(login);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get user rivals");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<UserBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<UserBO>>> GetUsersWorstRivalsAsync(string login)
        {
            try
            {
                var result = await FantasyClient.GetUsersWorstRivalsAsync(login);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get user rivals");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<UserBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<UserBO>>> GetUserFriendsAsync(string login)
        {
            try
            {
                var result = await FantasyClient.GetUserFriendsAsync(login);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get user rivals");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<UserBO>>(exception);
            }
        }

        internal async Task<ServiceResult<List<LineupBO>>> GetLineupsAsync(string login)
        {
            try
            {
                var result = await FantasyClient.GetLineupsAsync(login);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get lineups from " + login);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Lineups);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<LineupBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<LineupBO>>> GetLineupsFromcontestAsync(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetLineupsFromContestAsync(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get lineups from contest" + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Lineups);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<LineupBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<LineupBO>>> GetActiveLineupsAsync(string login)
        {
            try
            {
                var result = await FantasyClient.GetActiveLineupsAsync(login);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get lineups from " + login);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Lineups);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<LineupBO>>(exception);
            }
        }
        internal async Task<ServiceResult<ContestBO>> GetContest(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetContest(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get contest");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contest, result.Result.Lineups, result.Result.Games, result.Result.Starts);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<ContestBO>(exception);
            }
        }
        internal async Task<ServiceResult<List<ContestBO>>> GetContests(DateTime startDate)
        {
            try
            {
                var result = await FantasyClient.GetContests(startDate);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get contest");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Contests);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<GameBO>>> GetGamesFromContest(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetGamesFromContest(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get games from contest " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Games);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<GameBO>>(exception);
            }
        }
        internal async Task<ServiceResult<GameBO>> GetGameAsync(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetGame(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get game " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Game);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<GameBO>(exception);
            }
        }
        internal async Task<ServiceResult<List<TeamBO>>> GetTeamsFromGames(List<Game> games)
        {
            try
            {
                var result = await FantasyClient.GetTeamsFromGames(games);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get teams from games ");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Teams);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<TeamBO>>(exception);
            }
        }
        internal async Task<ServiceResult<GoalsBO>> GetGoalsfromContest(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetGoalsfromContest(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get goals from contest " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Goals);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<GoalsBO>(exception);
            }
        }
        internal async Task<ServiceResult<PlayerBO>> GetPlayer(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetPlayer(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get player " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Player);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<PlayerBO>(exception);
            }
        }
        internal async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromLineup(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetPlayersFromLineup(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get players from lineup " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Players);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<PlayerBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<GameBO>>> GetGamesfromTeam(Int64 id)
        {
            try
            {
                var result = await FantasyClient.GetGamesfromTeam(id);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get games from team " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Games);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<GameBO>>(exception);
            }
        }

        internal async Task<ServiceResult<List<PlayerBO>>> GetPlayersFromTeamsAsync(List<Team> teams)
        {
            try
            {
                var result = await FantasyClient.GetPlayersFromTeamsAsync(teams);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get games from teams");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Players);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<PlayerBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<NewsBO>>> GetNews(DateTime start, DateTime end)
        {
            try
            {
                var result = await FantasyClient.GetNews(start, end);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get news from dates");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.News);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<NewsBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<NewsBO>>> GetPlayerNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                var result = await FantasyClient.GetPlayerNews(id, start, end);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get news from player " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.News);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<NewsBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<NewsBO>>> GetTeamNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                var result = await FantasyClient.GetTeamNews(id, start, end);
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get news from team " + id);
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.News);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<NewsBO>>(exception);
            }
        }
        internal async Task<ServiceResult<List<ContestTypeBO>>> GetContestTypesAsync()
        {
            try
            {
                var result = await FantasyClient.GetContestTypesAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get contest types ");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await new Mapping.FantasyData.FantasyDataMapping().Create(result.Result.Types);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<List<ContestTypeBO>>(exception);
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
                    FantasyClient.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyDataServiceCore()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
