using Fantasy.API.DataAccess.Configurations;
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

        internal async Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(int teamId)
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

        internal async Task<ServiceResult<TeamResponse>> GetTeamAsync(int teamId)
        {
            try
            {
                Team t = dbContext.Teams.First(x => x.TeamId == teamId);
                t.Sport = dbContext.Sports.First(x => x.SportId == t.SportId);
                t.Leagues = dbContext.Leagues.Where(x => x.TeamTeamId == teamId).ToList();
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

        internal async Task<ServiceResult<ContestTypeResponse>> GetContesTypesAsync()
        {
            try
            {
                var ctContest = dbContext.ContestTypes.ToList();
                var result = new ContestTypeResponse()
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
                return ExceptionHandler<ContestTypeResponse>(ex);
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
    }
}
