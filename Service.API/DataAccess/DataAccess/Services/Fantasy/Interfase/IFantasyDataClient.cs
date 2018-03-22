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
        Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamAsync(Int64 teamId);
        Task<ServiceResult<PlayersResponse>> GetPlayersFromTeamsAsync(List<Models.MSSQL.Fantasy.Team> teams);
        Task<ServiceResult<TeamResponse>> GetTeamAsync(Int64 teamId);
        Task<ServiceResult<NotificationsResponse>> GetActiveNotificationsAsync();
        Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(Account user);
        Task<ServiceResult<NotificationsResponse>> GetUserActiveNotificationsAsync(string user);
        Task<ServiceResult<InformationsResponse>> GetInformationsAsync();
        Task<ServiceResult<InformationsResponse>> GetInformationsAsync(DateTime start, DateTime end);
        Task<ServiceResult<PromotionsResponse>> GetPromotionsAsync();
        Task<ServiceResult<DateTime>> GetNextContestTimeAsync(IEnumerable<Contest> contests);
        Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type);
        Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(double smallEntry, double bigEntry);
        Task<ServiceResult<ContestsResponse>> GetContestFilteredbyAsync(ContestType type, double smallEntry, double bigEntry);
        Task<ServiceResult<ContestTypesResponse>> GetContestTypesAsync();
        //TODO: Task<ServiceResult<ContestResponse>> GetContestFilteredby(Prizing Rule);
        Task<ServiceResult<UserResponse>> GetUserInfoAsync(string login);
        Task<ServiceResult<List<Account>>> GetUsersBestRivalsAsync(string login);
        Task<ServiceResult<List<Account>>> GetUsersWorstRivalsAsync(string login);
        Task<ServiceResult<List<Account>>> GetUserFriendsAsync(string login);
        Task<ServiceResult<LineupsResponse>> GetLineupsAsync(string login);
        Task<ServiceResult<LineupsResponse>> GetLineupsFromContestAsync(Int64 id);
        Task<ServiceResult<LineupsResponse>> GetActiveLineupsAsync(string login);
        Task<ServiceResult<ContestResponse>> GetContest(Int64 id);
        Task<ServiceResult<ContestsResponse>> GetContests(DateTime startDate);
        Task<ServiceResult<GamesResponse>> GetGamesFromContest(Int64 id);
        Task<ServiceResult<TeamsResponse>> GetTeamsFromGames(List<Models.MSSQL.Fantasy.Game> games);
        Task<ServiceResult<TeamsResponse>> GetTeamsFromGame(long gmeID);
        Task<ServiceResult<GoalsResponse>> GetGoalsfromContest(Int64 id);
        Task<ServiceResult<PlayerResponse>> GetPlayer(Int64 id);
        Task<ServiceResult<PlayersResponse>> GetPlayersFromLineup(Int64 id);
        Task<ServiceResult<GamesResponse>> GetGamesfromTeam(Int64 id);
        Task<ServiceResult<GameResponse>> GetGame(Int64 id);
        Task<ServiceResult<NewsResponse>> GetNews(DateTime start, DateTime end);
        Task<ServiceResult<NewsResponse>> GetPlayerNews(Int64 id, DateTime start, DateTime end);
        Task<ServiceResult<NewsResponse>> GetTeamNews(Int64 id, DateTime start, DateTime end);
        Task<ServiceResult<List<ContestGame>>> GetContestGamesAsync(Int64 contest);
        Task<ServiceResult<SportResponse>> GetSportAsync(long id);
        Task<ServiceResult<PositionResponse>> GetPositionAsync(long id);
        Task<ServiceResult<ClimaConditionResponse>> GetClimaConditionAsync(long id);
        Task<ServiceResult<VenueResponse>> GetVenueAsync(long id);
        Task<ServiceResult<ContestTypeResponse>> GetContestTypeAsync(long id);



        Task<ServiceResult<InformationResponse>> PostInformationAsync(Information info);
        Task<ServiceResult<PromotionResponse>> PostPromotionAsync(Promotion promo);
        Task<ServiceResult<ContestTypeResponse>> PostContestTypeAsync(ContestType ctype);
        Task<ServiceResult<SportResponse>> PostSportAsync(Sport sport);
        Task<ServiceResult<PositionResponse>> PostPositionAsync(Position position);
        Task<ServiceResult<ClimaConditionResponse>> PostClimaConditionAsync(ClimaConditions ccond);
        Task<ServiceResult<VenueResponse>> PostVenueAsync(Models.MSSQL.Fantasy.Venue venue);
        Task<ServiceResult<GoalResponse>> PostGoalAsync(Goal goal);
        Task<ServiceResult<NotificationResponse>> PostNotificationAsync(Notification notification);
        Task<ServiceResult<TeamResponse>> PostTeamAsync(Models.MSSQL.Fantasy.Team team);
        Task<ServiceResult<SingleNewsResponse>> PostNewsAsync(News News);
        Task<ServiceResult<LeagueResponse>> PostLeagueAsync(Models.MSSQL.Fantasy.League league);
        Task<ServiceResult<Models.MSSQL.Fantasy.InjuryResponse>> PostInjuryAsync(Models.MSSQL.Fantasy.Injury injury);
        Task<ServiceResult<LineupResponse>> PostLineupAsync(Models.MSSQL.Fantasy.LineUp lineup);
        Task<ServiceResult<UserResponse>> PostAccountAsync(Account user);
        Task<ServiceResult<PlayerResponse>> PostPlayerAsync(Models.MSSQL.Fantasy.Player player);
        Task<ServiceResult<GameResponse>> PostGameAsync(Models.MSSQL.Fantasy.Game game);
        Task<ServiceResult<ContestResponse>> PostContestAsync(Contest contest);


        Task<ServiceResult<InformationResponse>> PutInformationAsync(Information info);
        Task<ServiceResult<PromotionResponse>> PutPromotionAsync(Promotion promo);
        Task<ServiceResult<ContestTypeResponse>> PutContestTypeAsync(ContestType ctype);
        Task<ServiceResult<SportResponse>> PutSportAsync(Sport sport);
        Task<ServiceResult<PositionResponse>> PutPositionAsync(Position position);
        Task<ServiceResult<ClimaConditionResponse>> PutClimaConditionAsync(ClimaConditions ccond);
        Task<ServiceResult<VenueResponse>> PutVenueAsync(Models.MSSQL.Fantasy.Venue venue);
        Task<ServiceResult<GoalResponse>> PutGoalAsync(Goal goal);
        Task<ServiceResult<NotificationResponse>> PutNotificationAsync(Notification notification);
        Task<ServiceResult<TeamResponse>> PutTeamAsync(Models.MSSQL.Fantasy.Team team);
        Task<ServiceResult<SingleNewsResponse>> PutNewsAsync(News News);
        Task<ServiceResult<LeagueResponse>> PutLeagueAsync(Models.MSSQL.Fantasy.League league);
        Task<ServiceResult<Models.MSSQL.Fantasy.InjuryResponse>> PutInjuryAsync(Models.MSSQL.Fantasy.Injury injury);
        Task<ServiceResult<LineupResponse>> PutLineupAsync(Models.MSSQL.Fantasy.LineUp lineup);



        Task<ServiceResult<bool>> DeleteInformationAsync(Information info);
        Task<ServiceResult<bool>> DeletePromotionAsync(Promotion promo);


    }
};

        
