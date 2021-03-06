﻿using Fantasy.API.DataAccess.Configurations;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System;
using System.Net;
using System.Threading.Tasks;
using Fantasy.API.Utilities.HttpClient;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Fantasy.API.DataAccess.Services.Fantasy.Core
{
    public class DatabaseCore : BaseService, IDisposable
    {
        private bool _disposed = false;
        private readonly Response _response = new Response();
        private readonly IHttpClient _httpClientDatabase;
        private readonly FantasyContext dbContext = new FantasyContext();

        public DatabaseCore(IHttpClient httpClient = null)
        {
            _httpClientDatabase = httpClient ?? ExternalServiceContext.InstanceForHttpClientFantasyData;
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
                if (_httpClientDatabase != null)
                {
                    _httpClientDatabase.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion

        #region GET Section
        internal async Task<ServiceResult<ContestsResponse>> GetContestsAsync()
        {
            try
            {
                ContestsResponse result = new ContestsResponse();                
                List<Contest> list;
                dbContext.Configuration.ProxyCreationEnabled = false;
                var dbQuery = dbContext.Contests.ToList();
                foreach (Contest c in dbQuery)
                {
                    var cgames = dbContext.ContestGames.Where(x => x.ContestId == c.ContestId).Include("Game").ToList();
                    var clineups = dbContext.ContestLineups.Where(x => x.ContestId == c.ContestId).ToList();
                    c.ContestGame = cgames;
                    c.ContestLineups = clineups;
                }
                
                //dbQuery.Include(x => x.Games);
                list = dbQuery.ToList();
                result.Contests = dbQuery;                
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<ContestsResponse>> GetActiveContestsAsync()
        {
            try
            {
                ContestsResponse result = new ContestsResponse();
                dbContext.Configuration.ProxyCreationEnabled = false;
                var dbQuery = dbContext.Contests.ToList();
                foreach (Contest c in dbQuery)
                {
                    var cgames = dbContext.ContestGames.Where(x => x.ContestId == c.ContestId).Include("Game").ToList();
                    var clineups = dbContext.ContestLineups.Where(x => x.ContestId == c.ContestId).ToList();
                    c.ContestGame = cgames;
                    c.ContestLineups = clineups;
                }
                DateTime now = DateTime.Now;
                var active = new List<Contest>();
                foreach (Contest c in dbQuery)
                {
                    List<ContestGame> contGames = c.ContestGame.Where(x => x.Game.Scheduled > now).ToList();
                    if (contGames.Count > 0)
                    {
                        active.Add(c);
                    }
                }
                result.Contests = active;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(Int64 teamId)
        {            
            try
            {
                var result = new PlayersResponse()
                {
                    Players = dbContext.Players.Where(x => x.TeamId == teamId).ToList()
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Players from Team " + teamId);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayersResponse>(ex);
            }
        }

        internal async Task<ServiceResult<TeamResponse>> GetTeamAsync(Int64 teamId)
        {
            try
            {
                Team t = dbContext.Teams.Where(x => x.TeamId == teamId).First();
                t.Sport = dbContext.Sports.First(x => x.SportId == t.SportId);
                t.TeamLeagues = dbContext.TeamLeagues.Where(x => x.Id == teamId).ToList();
                t.Players = dbContext.Players.Where(x => x.TeamId == teamId).ToList();
                var result = new TeamResponse()
                {
                    Team = t
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Team " + teamId);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TeamResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamsAsync(List<Models.MSSQL.Fantasy.Team> teams)
        {
            try
            {
                List<Player> players = new List<Player>();
                foreach (Team t in teams)
                {
                    players.AddRange(dbContext.Players.Where(x => x.TeamId == t.TeamId).ToList());
                }
                var result = new PlayersResponse()
                {
                    Players = players
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Players from Teams");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayersResponse>(ex);
            }
        }

        internal async Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(Account user)
        {
            try
            {
                var result = new NotificationsResponse()
                {
                    Notifications = new List<Notification>()
                };
                result.Notifications = dbContext.Notifications.Where(x=>x.Active && x.Account.Login == user.Login).ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(string user)
        {
            try
            {
                var result = new NotificationsResponse()
                {
                    Notifications = new List<Notification>()
                };
                result.Notifications = dbContext.Notifications.Where(x => x.Active && x.Account.Login == user).Include("Account").ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<NotificationsResponse>> GetActiveNotificationsAsync()
        {
            try
            {
                var result = new NotificationsResponse()
                {
                    Notifications = new List<Notification>()
                };
                result.Notifications = dbContext.Notifications.Where(x=>x.Active).ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<InformationsResponse>> GetInformationsAsync()
        {
            try
            {
                var result = new InformationsResponse()
                {
                    Informations = new List<Information>()
                };
                result.Informations = dbContext.Informations.ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InformationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<PromotionsResponse>> GetPromotionsAsync()
        {
            try
            {
                var result = new PromotionsResponse()
                {
                    Promotions = new List<Promotion>()
                };
                result.Promotions = dbContext.Promotions.ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications ");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PromotionsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<InformationsResponse>> GetInformationsAsync(DateTime start, DateTime end)
        {
            try
            {
                var result = new InformationsResponse()
                {
                    Informations = new List<Information>()
                };
                result.Informations = dbContext.Informations.Where(x => x.FinalDate >= start || x.FinalDate >= end).ToList();
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Notifications");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InformationsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<DateTime>> GetNextContestTimeAsync(IEnumerable<Contest> contests)
        {
            try
            {
                DateTime result = new DateTime();
                foreach (Contest c in contests)
                {
                    var cgames = c.ContestGame.OrderBy(x => x.Game.Scheduled).ToList();
                    DateTime scheduledGame = cgames.First().Game.Scheduled;
                    if (scheduledGame < result)
                    {
                        result = scheduledGame;
                    }
                }

                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Next Contest Time");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<DateTime>(ex);
            }
        }
        internal async Task<ServiceResult<List<ContestGame>>> GetContestGamesAsync(Int64 contestId)
        {
            try
            {
                List<ContestGame> result = dbContext.ContestGames.Include("Game").Where(x => x.ContestId == contestId).ToList();

                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contest Games");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<List<ContestGame>>(ex);
            }
        }


        internal async Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type)
        {
            try
            {
                ContestsResponse actives = GetActiveContestsAsync().Result.Result;
                ContestsResponse result = new ContestsResponse();
                List<Contest> filtered = actives.Contests.Where(x => x.ContestType == type).ToList();
                result.Contests = filtered;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(double smallEntry, double bigEntry)
        {
            try
            {
                ContestsResponse actives = GetActiveContestsAsync().Result.Result;
                ContestsResponse result = new ContestsResponse();
                List<Contest> filtered = actives.Contests.Where(x => x.EntryFee >= smallEntry && x.EntryFee <= bigEntry).ToList();
                result.Contests = filtered;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type, double smallEntry, double bigEntry)
        {
            try
            {
                ContestsResponse actives = GetActiveContestsAsync().Result.Result;
                ContestsResponse result = new ContestsResponse();
                List<Contest> filtered = actives.Contests.Where(x => x.ContestType == type && x.EntryFee >= smallEntry && x.EntryFee <= bigEntry).ToList();
                result.Contests = filtered;
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<ContestTypesResponse>> GetContesTypesAsync()
        {
            try
            {
                var ctContest = dbContext.ContestTypes.ToList();
                var result = new ContestTypesResponse()
                {
                    Types = ctContest
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contest Type");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestTypesResponse>(ex);
            }
        }

        internal async Task<ServiceResult<UserResponse>> GetUserInfoAsync(string login)
        {
            try
            {
                Account user = dbContext.Accounts.Where(x => x.Login == login).First();
                List<AccountFriends> Accountfriends = dbContext.AccountFriends.Where(x => x.AccountLogin == user.Login || x.AccountLogin1 == user.Login).ToList();
                List<Account> Friends = new List<Account>();
                foreach (AccountFriends af in Accountfriends)
                {
                    if (af.AccountLogin != user.Login)
                    {
                        Account friend = dbContext.Accounts.Where(x => x.Login == af.AccountLogin).First();
                        Friends.Add(friend);
                    }
                    else
                    {
                        Account friend = dbContext.Accounts.Where(x => x.Login == af.AccountLogin1).First();
                        Friends.Add(friend);
                    }
                }
                UserResponse result = new UserResponse()
                {
                    User = user,
                    Friends = Friends,
                    Money = user.Money,
                    Point = user.Point
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Info");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<UserResponse>(ex);
            }
        }

        internal async Task<ServiceResult<List<Account>>> GetUsersBestRivalsAsync(string login)
        {
            try
            {
                UserResponse userR = GetUserInfoAsync(login).Result.Result;
                List<Account> result = userR.Friends.Where(x => x.Money >= userR.Money).ToList();

                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Best Rivals");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<List<Account>>(ex);
            }
        }

        internal async Task<ServiceResult<List<Account>>> GetUsersWorstRivalsAsync(string login)
        {
            try
            {
                UserResponse userR = GetUserInfoAsync(login).Result.Result;
                List<Account> result = userR.Friends.Where(x => x.Money < userR.Money).ToList();

                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Worst Rivals");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<List<Account>>(ex);
            }
        }

        internal async Task<ServiceResult<List<Account>>> GetUserFriendsAsync(string login)
        {
            try
            {
                UserResponse userR = GetUserInfoAsync(login).Result.Result;
                List<Account> result = userR.Friends;

                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Friends");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<List<Account>>(ex);
            }
        }
        internal async Task<ServiceResult<LineupsResponse>> GetLineupsAsync(string login)
        {
            try
            {
                UserResponse userR = GetUserInfoAsync(login).Result.Result;
                List<LineUp> lineups = dbContext.LineUps.Where(x => x.Account.Login == login).ToList();
                LineupsResponse result = new LineupsResponse()
                {
                    Lineups = lineups
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Lineups");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LineupsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LineupsResponse>> GetLineupsFromContestAsync(Int64 id)
        {
            try
            {
                List<ContestLineup> clinups = dbContext.ContestLineups.Where(x => x.ContestId == id).ToList();
                List<LineUp> lineups = new List<LineUp>();
                foreach (ContestLineup clp in clinups)
                {
                    LineUp lp = dbContext.LineUps.Where(x => x.LineUpId == clp.LineUpId).Include("Account").First();
                    List<PlayerLineup> pls = dbContext.PlayerLineups.Where(x => x.LineupId == clp.LineUpId).ToList();
                    lp.PlayerLineup = pls;
                    lineups.Add(lp);
                }
                LineupsResponse result = new LineupsResponse()
                {
                    Lineups = lineups
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Lineups from contest " + id);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LineupsResponse>(ex);
            }
        }

        internal async Task<ServiceResult<LineupsResponse>> GetActiveLineupsAsync(string login)
        {
            try
            {
                ContestsResponse Activecontests = GetActiveContestsAsync().Result.Result;
                List<LineUp> lineups = dbContext.LineUps.Where(x => x.Account.Login == login).ToList();
                List<LineUp> active = new List<LineUp>();
                foreach (LineUp lp in lineups)
                {
                    List<ContestLineup> lpcs = dbContext.ContestLineups.Where(x => x.LineUpId == lp.LineUpId).ToList();
                    foreach (ContestLineup clp in lpcs)
                    {
                        List<Contest> cont = Activecontests.Contests.Where(x => x.ContestId == clp.ContestId).ToList();
                        if (cont.Count > 0)
                        {
                            active.Add(lp);
                        }
                    }
                }
                LineupsResponse result = new LineupsResponse()
                {
                    Lineups = active
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Lineups");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LineupsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LineupsResponse>> GetLineupsofContest(Int64 id)
        {
            try
            {
                List<ContestLineup> clups = dbContext.ContestLineups.Where(x => x.ContestId == id).ToList();
                List<LineUp> active = new List<LineUp>();
                foreach (ContestLineup lp in clups)
                {
                    LineUp l = dbContext.LineUps.Where(x => x.LineUpId == lp.LineUpId).First();

                }
                LineupsResponse result = new LineupsResponse()
                {
                    Lineups = active
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting User Lineups");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LineupsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestResponse>> GetContest(Int64 id)
        {
            try
            {
                Contest c = dbContext.Contests.Where(x => x.ContestId == id).First();
                List<ContestLineup> clineups = dbContext.ContestLineups.Where(x => x.ContestId == id).ToList();
                List<ContestGame> cgames = dbContext.ContestGames.Where(x => x.ContestId == id).ToList();
                List<Game> games = new List<Game>();
                List<LineUp> lineups = new List<LineUp>();
                foreach (ContestLineup clp in clineups)
                {
                    LineUp l = dbContext.LineUps.Where(x => x.LineUpId == clp.LineUpId).First();
                    lineups.Add(l);
                }
                foreach (ContestGame cg in cgames)
                {
                    Game g = dbContext.Games.Where(x => x.GameId == cg.GameId).First();
                    games.Add(g);
                }
                DateTime start = games.OrderBy(x => x.Scheduled).First().Scheduled;

                ContestResponse result = new ContestResponse()
                {
                    Contest = c,
                    Games = games,
                    Lineups = lineups,
                    Starts = start
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contest");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestsResponse>> GetContests(DateTime date)
        {
            try
            {
                List<Game> games = dbContext.Games.Where(x => x.Scheduled == date).ToList();
                List<Contest> contests = new List<Contest>();

                foreach (Game g in games)
                {
                    List<ContestGame> cgames = dbContext.ContestGames.Where(x => x.GameId == g.GameId).ToList();
                    foreach (ContestGame cg in cgames)
                    {
                        contests.Add(dbContext.Contests.Where(x => x.ContestId == cg.ContestId).First());
                    }
                }
                ContestsResponse result = new ContestsResponse()
                {
                    Contests = contests
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contests");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GamesResponse>> GetGamesFromContest(Int64 id)
        {
            try
            {
                List<Game> games = new List<Game>();
                List<ContestGame> cgames = dbContext.ContestGames.Where(x => x.ContestId == id).ToList();
                foreach (ContestGame cg in cgames)
                {
                    games.Add(dbContext.Games.Where(x => x.GameId == cg.GameId).First());
                }
                GamesResponse result = new GamesResponse()
                {
                    Games = games
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Games from Contest");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GamesResponse>(ex);
            }
        }
        internal async Task<ServiceResult<TeamsResponse>> GetTeamsFromGames(List<Game> games)
        {
            try
            {
                List<Team> teams = new List<Team>();
                foreach (Game g in games)
                {
                    Team home = dbContext.Teams.Where(x => x.TeamId == g.TeamTeamId).First();
                    Team away = dbContext.Teams.Where(x => x.TeamId == g.TeamTeamId1).First();
                    home.Players = dbContext.Players.Where(x => x.TeamId == home.TeamId).ToList();
                    away.Players = dbContext.Players.Where(x => x.TeamId == away.TeamId).ToList();
                    teams.Add(home);
                    teams.Add(away);
                }
                TeamsResponse result = new TeamsResponse()
                {
                    Teams = teams
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Teams from Games");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TeamsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<TeamsResponse>> GetTeamsFromGame(long gameID)
        {
            try
            {
                List<Team> teams = new List<Team>();
                Game g = dbContext.Games.Where(x => x.GameId == gameID).First();

                Team home = dbContext.Teams.Where(x => x.TeamId == g.TeamTeamId).First();
                Team away = dbContext.Teams.Where(x => x.TeamId == g.TeamTeamId1).First();
                home.Players = dbContext.Players.Where(x => x.TeamId == home.TeamId).ToList();
                away.Players = dbContext.Players.Where(x => x.TeamId == away.TeamId).ToList();
                teams.Add(home);
                teams.Add(away);

                TeamsResponse result = new TeamsResponse()
                {
                    Teams = teams
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Teams from Games");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TeamsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeam(Int64 id)
        {
            try
            {
                Team team = dbContext.Teams.Where(x => x.TeamId == id).First();
                List<Player> players = dbContext.Players.Where(x => x.TeamId == team.TeamId).ToList();
               
                PlayersResponse result = new PlayersResponse()
                {
                    Players = players
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Players from Team");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayersResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeams(List<Team> teams)
        {
            try
            {
                List<Player> players = new List<Player>();
                foreach (Team t in teams)
                {
                    List<Player> plyrs = dbContext.Players.Where(x => x.TeamId == t.TeamId).ToList();
                    foreach (Player p in plyrs)
                    {
                        players.Add(p);
                    }
                }
                PlayersResponse result = new PlayersResponse()
                {
                    Players = players
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Players from Team");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayersResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GoalsResponse>> GetGoalsfromContest(Int64 id)
        {
            try
            {
                ContestGame cg = dbContext.ContestGames.Where(x => x.ContestId == id).First();
                Game g = dbContext.Games.Where(x => x.GameId == cg.GameId).First();
                Team t = dbContext.Teams.Where(x=>x.TeamId == g.TeamTeamId).First();
                List<Goal> goals = dbContext.Goals.Where(x => x.SportId == t.SportId).ToList();
                GoalsResponse result = new GoalsResponse()
                {
                    Goals = goals
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Goals from Contest");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GoalsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayerResponse>> GetPlayer(Int64 id)
        {
            try
            {
                Player p = dbContext.Players.Where(x => x.PlayerId == id).First();
                p.Position = dbContext.Positions.Where(x => x.PositionId == p.PositionId).First();
                p.Team = dbContext.Teams.Where(x => x.TeamId == p.TeamId).First();
                p.PlayerLineups = dbContext.PlayerLineups.Where(x => x.PlayerId == p.PlayerId).ToList();
                PlayerResponse result = new PlayerResponse()
                {
                    Player = p
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Player");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayerResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromLineup(Int64 id)
        {
            try
            {
                List<PlayerLineup> plineups = dbContext.PlayerLineups.Where(x => x.LineupId == id).ToList();
                List<Player> players = new List<Player>();
                foreach (PlayerLineup pl in plineups)
                {
                    Player p = dbContext.Players.Where(x => x.PlayerId == pl.PlayerId).First();
                    p.Position = dbContext.Positions.Where(x => x.PositionId == p.PositionId).First();
                    p.Team = dbContext.Teams.Where(x => x.TeamId == p.TeamId).First();
                    p.PlayerLineups = dbContext.PlayerLineups.Where(x => x.PlayerId == p.PlayerId).ToList();
                    players.Add(p);
                }
                PlayersResponse result = new PlayersResponse()
                {
                    Players = players
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Players from Lineup");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayersResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GamesResponse>> GetGamesfromTeam(Int64 id)
        {
            try
            {
                List<Game> games = dbContext.Games.Where(x => x.TeamTeamId == id || x.TeamTeamId1 == id).ToList();
                GamesResponse result = new GamesResponse()
                {
                    Games = games
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Games from team");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GamesResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GameResponse>> GetGame(Int64 id)
        {
            try
            {
                Game game = dbContext.Games.Where(x => x.GameId == id).First();
                GameResponse result = new GameResponse()
                {
                    Game = game
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Game" + id);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GameResponse>(ex);
            }
        }
        internal async Task<ServiceResult<NewsResponse>> GetNews(DateTime start, DateTime end)
        {
            try
            {
                List<News> news = dbContext.News.Where(x => x.Date >= start && x.Date <= end).ToList();
                NewsResponse result = new NewsResponse()
                {
                    News = news
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting News");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NewsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<NewsResponse>> GetPlayerNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                List<NewsPlayer> playerNews = dbContext.PlayerNews.Where(x => x.PlayerId == id).ToList();
                List<News> news = new List<News>();
                foreach (NewsPlayer np in playerNews)
                {
                    News n = dbContext.News.Where(x => x.NewsId == np.NewsId && x.Date >= start && x.Date <= end).First();
                    if (n != null)
                    {
                        news.Add(n);
                    }
                }
                NewsResponse result = new NewsResponse()
                {
                    News = news
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting News from player" + id);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NewsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<NewsResponse>> GetTeamNews(Int64 id, DateTime start, DateTime end)
        {
            try
            {
                List<News> news = new List<News>();

                // Team News
                List<NewsTeam> teamNews = dbContext.TeamNews.Where(x => x.TeamId == id).ToList();
                foreach (NewsTeam nt in teamNews)
                {
                    news.Add(dbContext.News.Where(x => x.NewsId == nt.NewsId && x.Date >= start && x.Date <= end).First());
                }

                // Team Players News
                List<Player> players = dbContext.Players.Where(x => x.TeamId == id).ToList();
                List<NewsPlayer> playerNews = new List<NewsPlayer>();
                foreach (Player p in players)
                {
                    List<NewsPlayer> pNews = dbContext.PlayerNews.Where(x => x.PlayerId == id).ToList();
                    playerNews.AddRange(pNews);
                }
                foreach (NewsPlayer np in playerNews)
                {
                    News n = dbContext.News.Where(x =>x.NewsId == np.NewsId && x.Date >= start && x.Date <= end).First();
                    if (n != null)
                    {
                        news.Add(n);
                    }
                }

                NewsResponse result = new NewsResponse()
                {
                    News = news
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting News from team" + id);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NewsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<SportResponse>> GetSport(Int64 id)
        {
            try
            {
                Sport sp = dbContext.Sports.Where(x => x.SportId == id).First();
                SportResponse result = new SportResponse()
                {
                    Sport = sp
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Sport");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<SportResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PositionResponse>> GetPositionAsync(long id)
        {
            try
            {
                Position p = dbContext.Positions.Where(x => x.PositionId == id).First();
                PositionResponse result = new PositionResponse()
                {
                    Position = p
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Position");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PositionResponse>(ex);
            }
        }

        internal async Task<ServiceResult<VenueResponse>> GetVenueAsync(long id)
        {
            try
            {
                Venue v = dbContext.Venues.Where(x => x.VenueId == id).First();
                VenueResponse result = new VenueResponse()
                {
                    Venue = v
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Venue");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<VenueResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestTypeResponse>> GetContestTypeAsync(long id)
        {
            try
            {
                ContestType v = dbContext.ContestTypes.Where(x => x.ContestTypeId == id).First();
                ContestTypeResponse result = new ContestTypeResponse()
                {
                    Type = v
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Contest Type");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestTypeResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ClimaConditionResponse>> GetClimaConditionAsync(long id)
        {
            try
            {
                ClimaConditions cc = dbContext.ClimaConditions.Where(x => x.ClimaConditionsId == id).First();
                ClimaConditionResponse result = new ClimaConditionResponse()
                {
                    ClimaCondition = cc
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting Clima Condition");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ClimaConditionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LeagueResponse>> GetLeagueAsync(long id)
        {
            try
            {
                League lg = dbContext.Leagues.Where(x => x.LeagueId == id).First();
                LeagueResponse result = new LeagueResponse()
                {
                    League = lg
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting League");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LeagueResponse>(ex);
            }
        }
        internal async Task<ServiceResult<Models.MSSQL.Fantasy.InjuryResponse>> GetInjuryAsync(long id)
        {
            try
            {
                Injury inj = dbContext.Injuries.Where(x => x.InjuryId == id).First();
                InjuryResponse result = new InjuryResponse()
                {
                    Injury = inj
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in getting an Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InjuryResponse>(ex);
            }
        }
        #endregion

        #region POST Section

        internal async Task<ServiceResult<InformationResponse>> PostInformationAsync(Information info)
        {
            try
            {
                dbContext.Informations.Add(info);
                dbContext.SaveChanges();
                var result = new InformationResponse()
                {
                    Information = info
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Information");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InformationResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PromotionResponse>> PostPromotionAsync(Promotion promo)
        {
            try
            {
                dbContext.Promotions.Add(promo);
                dbContext.SaveChanges();
                var result = new PromotionResponse()
                {
                    Promotion = promo
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Information");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PromotionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestTypeResponse>> PostContestTypeAsync(ContestType ctype)
        {
            try
            {
                dbContext.ContestTypes.Add(ctype);
                dbContext.SaveChanges();
                var result = new ContestTypeResponse()
                {
                    Type = ctype
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Contest Type");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestTypeResponse>(ex);
            }
        }
        internal async Task<ServiceResult<SportResponse>> PostSportAsync(Sport sport)
        {
            try
            {
                dbContext.Sports.Add(sport);
                dbContext.SaveChanges();
                var result = new SportResponse()
                {
                    Sport = sport
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Sport");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<SportResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PositionResponse>> PostPositionAsync(Position position)
        {
             try
            {
                dbContext.Positions.Add(position);
                dbContext.SaveChanges();
                var result = new PositionResponse()
                {
                    Position = position
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Position");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PositionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ClimaConditionResponse>> PostClimaConditionAsync(ClimaConditions ccond)
        {
            try
            {
                dbContext.ClimaConditions.Add(ccond);
                dbContext.SaveChanges();
                var result = new ClimaConditionResponse()
                {
                    ClimaCondition = ccond
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Clima Condition");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ClimaConditionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<VenueResponse>> PostVenueAsync(Models.MSSQL.Fantasy.Venue venue)
        {
            try
            {
                dbContext.Venues.Add(venue);
                dbContext.SaveChanges();
                var result = new VenueResponse()
                {
                    Venue = venue
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Venue");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<VenueResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GoalResponse>> PostGoalAsync(Goal goal)
        {
            try
            {
                dbContext.Goals.Add(goal);
                dbContext.SaveChanges();
                var result = new GoalResponse()
                {
                    Goal = goal
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Goal");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GoalResponse>(ex);
            }
        }
        internal async Task<ServiceResult<NotificationResponse>> PostNotificationAsync(Notification notification)
        {
            try
            {
                dbContext.Notifications.Add(notification);
                dbContext.SaveChanges();
                var result = new NotificationResponse()
                {
                    Notification = notification
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Notification");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationResponse>(ex);
            }
        }
        internal async Task<ServiceResult<TeamResponse>> PostTeamAsync(Team team)
        {
            try
            {
                dbContext.Teams.Add(team);
                dbContext.SaveChanges();
                var result = new TeamResponse()
                {
                    Team = team
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Team");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TeamResponse>(ex);
            }
        }
        internal async Task<ServiceResult<SingleNewsResponse>> PostNewsAsync(News News)
        {
            try
            {
                dbContext.News.Add(News);
                dbContext.SaveChanges();
                var result = new SingleNewsResponse()
                {
                    News = News
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating News");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<SingleNewsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LeagueResponse>> PostLeagueAsync(League league)
        {
            try
            {
                dbContext.Leagues.Add(league);
                dbContext.SaveChanges();
                var result = new LeagueResponse()
                {
                    League = league
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating League");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LeagueResponse>(ex);
            }
        }
        internal async Task<ServiceResult<Models.MSSQL.Fantasy.InjuryResponse>> PostInjuryAsync(Models.MSSQL.Fantasy.Injury injury)
        {
            try
            {
                dbContext.Injuries.Add(injury);
                dbContext.SaveChanges();
                var result = new InjuryResponse()
                {
                    Injury = injury
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InjuryResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LineupResponse>> PostLineupAsync(LineUp lineup)
        {
            try
            {
                dbContext.LineUps.Add(lineup);
                dbContext.SaveChanges();
                var result = new LineupResponse()
                {
                    Lineup = lineup
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LineupResponse>(ex);
            }
        }
        internal async Task<ServiceResult<UserResponse>> PostAccountAsync(Account user)
        {
            try
            {
                dbContext.Accounts.Add(user);
                dbContext.SaveChanges();
                var result = new UserResponse()
                {
                    User = user,
                    Money = 20.5,
                    Point = 30
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<UserResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayerResponse>> PostPlayerAsync(Models.MSSQL.Fantasy.Player player)
        {
            try
            {
                dbContext.Players.Add(player);
                dbContext.SaveChanges();
                var result = new PlayerResponse()
                {
                    Player = player
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Player");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayerResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GameResponse>> PostGameAsync(Models.MSSQL.Fantasy.Game game)
        {
            try
            {
                dbContext.Games.Add(game);
                dbContext.SaveChanges();
                var result = new GameResponse
                {
                    Game = game
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Game");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GameResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestResponse>> PostContestAsync(Contest contest)
        {
            try
            {
                dbContext.Contests.Add(contest);
                dbContext.SaveChanges();
                var result = new ContestResponse()
                {
                    Contest = contest
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in creating Contest");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }
        #endregion

        #region PUT Section

        internal async Task<ServiceResult<InformationResponse>> PutInformationAsync(Information info)
        {
            try
            {
                dbContext.Entry(info).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new InformationResponse()
                {
                    Information = info
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Information");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InformationResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PromotionResponse>> PutPromotionAsync(Promotion promo)
        {
            try
            {
                dbContext.Entry(promo).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new PromotionResponse()
                {
                    Promotion = promo
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Promotion");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PromotionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestTypeResponse>> PutContestTypeAsync(ContestType ctype)
        {
            try
            {
                dbContext.Entry(ctype).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new ContestTypeResponse()
                {
                    Type = ctype
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Contest Type");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestTypeResponse>(ex);
            }
        }
        internal async Task<ServiceResult<SportResponse>> PutSportAsync(Sport sport)
        {
            try
            {
                dbContext.Entry(sport).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new SportResponse()
                {
                    Sport = sport
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Sport");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<SportResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PositionResponse>> PutPositionAsync(Position position)
        {
            try
            {
                dbContext.Entry(position).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new PositionResponse()
                {
                    Position = position
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Position");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PositionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ClimaConditionResponse>> PutClimaConditionAsync(ClimaConditions ccond)
        {
            try
            {
                dbContext.Entry(ccond).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new ClimaConditionResponse()
                {
                    ClimaCondition = ccond
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Clima Condition");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ClimaConditionResponse>(ex);
            }
        }
        internal async Task<ServiceResult<VenueResponse>> PutVenueAsync(Models.MSSQL.Fantasy.Venue venue)
        {
            try
            {
                dbContext.Entry(venue).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new VenueResponse()
                {
                    Venue = venue
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Venue");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<VenueResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GoalResponse>> PutGoalAsync(Goal goal)
        {
            try
            {
                dbContext.Entry(goal).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new GoalResponse()
                {
                    Goal = goal
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Goal");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GoalResponse>(ex);
            }
        }
        internal async Task<ServiceResult<NotificationResponse>> PutNotificationAsync(Notification notification)
        {
            try
            {
                dbContext.Entry(notification).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new NotificationResponse()
                {
                    Notification = notification
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Notification");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<NotificationResponse>(ex);
            }
        }
        internal async Task<ServiceResult<TeamResponse>> PutTeamAsync(Team team)
        {
            try
            {
                dbContext.Entry(team).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new TeamResponse()
                {
                    Team = team
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Team");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TeamResponse>(ex);
            }
        }
        internal async Task<ServiceResult<SingleNewsResponse>> PutNewsAsync(News News)
        {
            try
            {
                dbContext.Entry(News).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new SingleNewsResponse()
                {
                    News = News
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating News");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<SingleNewsResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LeagueResponse>> PutLeagueAsync(League league)
        {
            try
            {
                dbContext.Entry(league).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new LeagueResponse()
                {
                    League = league
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating League");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LeagueResponse>(ex);
            }
        }
        internal async Task<ServiceResult<Models.MSSQL.Fantasy.InjuryResponse>> PutInjuryAsync(Models.MSSQL.Fantasy.Injury injury)
        {
            try
            {
                dbContext.Entry(injury).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new InjuryResponse()
                {
                    Injury = injury
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<InjuryResponse>(ex);
            }
        }
        internal async Task<ServiceResult<LineupResponse>> PutLineupAsync(LineUp lineup)
        {
            try
            {
                dbContext.Entry(lineup).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new LineupResponse()
                {
                    Lineup = lineup
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<LineupResponse>(ex);
            }
        }
        internal async Task<ServiceResult<UserResponse>> PutAccountAsync(Account user)
        {
            try
            {
                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new UserResponse()
                {
                    User = user,
                    Money = 20.5,
                    Point = 30
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Injury");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<UserResponse>(ex);
            }
        }
        internal async Task<ServiceResult<PlayerResponse>> PutPlayerAsync(Models.MSSQL.Fantasy.Player player)
        {
            try
            {
                dbContext.Entry(player).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new PlayerResponse()
                {
                    Player = player
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Player");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<PlayerResponse>(ex);
            }
        }
        internal async Task<ServiceResult<GameResponse>> PutGameAsync(Models.MSSQL.Fantasy.Game game)
        {
            try
            {
                dbContext.Entry(game).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new GameResponse
                {
                    Game = game
                };
                if (result != null)
                    return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Game");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<GameResponse>(ex);
            }
        }
        internal async Task<ServiceResult<ContestResponse>> PutContestAsync(Contest contest)
        {
            try
            {
                dbContext.Entry(contest).State = EntityState.Modified;
                dbContext.SaveChanges();
                var result = new ContestResponse()
                {
                    Contest = contest
                };
                if (result != null)
                {
                    var res = await ServiceOkAsync(result);
                    return res;
                }
                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in updating Contest");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<ContestResponse>(ex);
            }
        }

        #endregion

        #region Delete Section

        internal async Task<ServiceResult<bool>> DeleteInformationAsync(Information info)
        {
            try
            {
                var result = true;
                try
                {
                    dbContext.Informations.Remove(info);
                    dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    result = false;
                }
                return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in deleting Information");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<bool>(ex);
            }
        }

        internal async Task<ServiceResult<bool>> DeletePromotionAsync(Promotion promo)
        {
            try
            {
                var result = true;
                try
                {
                    dbContext.Promotions.Remove(promo);
                    dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    result = false;
                }
                return await ServiceOkAsync(result);

                throw new ServiceException(httpStatusCode: HttpStatusCode.InternalServerError,
                        message: "HandleResponse failed in deleting Promotion");
            }
            catch (Exception ex)
            {
                return ExceptionHandler<bool>(ex);
            }
        }
        #endregion 
    }
}
