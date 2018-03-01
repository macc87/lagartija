using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Dtos.Response.FantasyData;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Fantasy.API.Service.Mapping.ResponseMapping
{
    /// <summary>
    /// 
    /// </summary>
    public class DtoFactoryResponse
    {

        /// <summary>
        /// Service part
        /// </summary>


        /*
         * /// <summary>
        /// Create a Task with a Injury Dto 
        /// </summary>
        /// <param name="poco">Injuries Bussiness Object</param>
        /// <returns>Contest Dto</returns>
        public async Task<InjuryDto> Create(InjuriesBO poco)
        {
            var result = new InjuryDto
            {
                _comment = poco._comment
            };
            result.League = await Create(poco.League);
            result.teams = await Create(poco.teams);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a InjuriesTeam Dto
        /// </summary>
        /// <param name="teams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InjuriesTeamDto>> Create(IEnumerable<InjuriesTeamBO> teams)
        {
            var result = new List<InjuriesTeamDto>();
            return await Task.FromResult(result);
        }
        */
        /// <summary>
        /// Create a Task with a League Dto 
        /// </summary>
        /// <param name="league">League Bussiness Object</param>
        /// <returns>League Dto</returns>
        public async Task<LeagueDto> Create(LeagueBO league)
        {
            var result = new LeagueDto
            {
                alias = league.alias,
                id = league.id,
                name = league.name
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// DB part
        /// </summary>

        /// <summary>
        /// Create a Task with a Contest Dto 
        /// </summary>
        /// <param name="poco">Contest Bussiness Object</param>
        /// <returns>Contest Dto</returns>
        public async Task<ContestDto> Create(ContestBO poco)
        {
            var result = new ContestDto
            {
                Name = poco.Name,
                EntryFee = poco.EntryFee,
                MaxCapacity = poco.MaxCapacity,
                SalaryCap = poco.SalaryCap,
                SignedUp = poco.SignedUp,
                ContestId = poco.ContestId,
            };
            result.Games = await Create(poco.Games);
            result.LineUpsDto = await Create(poco.LineUps);
            result.ContestTypeId = await Create(poco.ContestTypeId);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a list of Games Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Games Dto's</returns>
        public async Task<IEnumerable<GameDto>> Create(IEnumerable<GameBO> poco)
        {
            var result = new List<GameDto>();
            foreach (GameBO game in poco)
            {
                GameDto gDto = new GameDto()
                {
                    GameId = game.GameId,
                    Humidity = game.Humidity,
                    Scheduled = game.Scheduled,
                    Temperture = game.Temperture,
                };
                //gDto.AwayTeam = await Create(game.AwayTeam);
                gDto.AwayTeam = new TeamDto()
                {
                    TeamId = game.AwayTeam.TeamId,
                    TeamLogo = game.AwayTeam.TeamLogo,
                    TeamName = game.AwayTeam.TeamName
                };
                gDto.HomeTeam = new TeamDto()
                {
                    TeamId = game.AwayTeam.TeamId,
                    TeamLogo = game.AwayTeam.TeamLogo,
                    TeamName = game.AwayTeam.TeamName
                };
                //gDto.HomeTeam = await Create(game.HomeTeam);
                gDto.Venue = await Create(game.Venue);
                gDto.ClimaCondition = await Create(game.ClimaCondition);
                result.Add(gDto);
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a list of Lineups Dto's 
        /// </summary>
        /// <param name="poco">List of Lineup Bussiness Object</param>
        /// <returns>List of Lineups Dto's</returns>
        public async Task<IEnumerable<LineupDto>> Create(IEnumerable<LineupBO> poco)
        {
            var result = new List<LineupDto>();
            foreach (LineupBO lbo in poco)
            {
                LineupDto luDto = new LineupDto()
                {
                    LineUpId = lbo.LineUpId,
                };
                luDto.User = await Create(lbo.User);
                List<PlayerDto> players = new List<PlayerDto>();
                foreach (PlayerBO pBo in lbo.Players)
                {
                    PlayerDto pDto = new PlayerDto()
                    {
                        FirstName = pBo.FirstName,
                        LastName = pBo.LastName,
                        Photo = pBo.Photo,
                        PlayerId = pBo.PlayerId,
                        Position = new PositionDto()
                        {
                            PositionId = pBo.Position.PositionId,
                            PositionName = pBo.Position.PositionName,
                            Sport = new SportDto()
                            {
                                Name = pBo.Position.Sport.Name,
                                SportId = pBo.Position.Sport.SportId
                            }
                        },
                        PreferredName = pBo.PreferredName,
                        Salary = pBo.Salary,
                        Team = new TeamDto()
                        {
                            TeamId = pBo.Team.TeamId,
                            TeamLogo = pBo.Team.TeamLogo,
                            TeamName = pBo.Team.TeamName,
                            Sport = new SportDto()
                            {
                                Name = pBo.Position.Sport.Name,
                                SportId = pBo.Position.Sport.SportId
                            }
                        }
                    };
                    players.Add(pDto);
                }
                luDto.Players = players;
                result.Add(luDto);
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a User 
        /// </summary>
        /// <param name="user">User Bussiness Object</param>
        /// <returns>User Dto</returns>
        public async Task<UserDto> Create(UserBO user)
        {
            UserDto result = new UserDto()
            {
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                Money = user.Money,
                Points = user.Point,
                _comment = ""
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a List of Users 
        /// </summary>
        /// <param name="users">User Bussiness Object</param>
        /// <returns>List of User Dto's</returns>
        public async Task<List<UserDto>> Create(IEnumerable<UserBO> users)
        {
            List<UserDto> listUser = new List<UserDto>();
            foreach (UserBO uBO in users)
            {
                UserDto result = new UserDto()
                {
                    Email = uBO.Email,
                    Login = uBO.Login,
                    Password = uBO.Password,
                    Money = uBO.Money,
                    Points = uBO.Point,
                    _comment = ""
                };
                listUser.Add(result);
            }
            return await Task.FromResult(listUser);
        }

        /// <summary>
        /// Create a Task with a Contest Type Dto
        /// </summary>
        /// <param name="cType">Contest Type Bussiness Object</param>
        /// <returns>Contest Type Dto</returns>
        public async Task<ContestTypeDto> Create(ContestTypeBO cType)
        {
            var result = new ContestTypeDto
            {
                ContestTypeId = cType.ContestTypeId,
                Type = cType.Type
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a Venue Dto
        /// </summary>
        /// <param name="poco">Venue Bussiness Object</param>
        /// <returns>Venue Dto</returns>
        public async Task<VenueDto> Create(VenueBO poco)
        {
            var result = new VenueDto
            {
                Name = poco.Name,
                Country = poco.Country,
                State = poco.State,
                Surface = poco.Surface,
                VenueId = poco.VenueId
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a Clima Condition Dto
        /// </summary>
        /// <param name="poco">Clima Condition Bussiness Object</param>
        /// <returns>Clima Condition Dto</returns>
        public async Task<ClimaConditionsDto> Create(ClimaConditionsBO poco)
        {
            var result = new ClimaConditionsDto
            {
                ClimaId = poco.ClimaId,
                Condition = poco.Condition
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a Team Dto
        /// </summary>
        /// <param name="poco">Team Bussiness Object</param>
        /// <returns>Team Dto</returns>
        public async Task<TeamDto> Create(TeamBO poco)
        {
            var result = new TeamDto
            {
                TeamId = poco.TeamId,
                TeamLogo = poco.TeamLogo,
                TeamName = poco.TeamName,
            };
            result.Players = await Create(poco.Players);
            result.Sport = await Create(poco.Sport);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a List of Team Dto
        /// </summary>
        /// <param name="poco">Team Bussiness Object</param>
        /// <returns>Team Dto</returns>
        public async Task<List<TeamDto>> Create(IEnumerable<TeamBO> poco)
        {
            List<TeamDto> teamsDto = new List<TeamDto>();
            foreach (TeamBO tBO in poco)
            {
                var result = new TeamDto
                {
                    TeamId = tBO.TeamId,
                    TeamLogo = tBO.TeamLogo,
                    TeamName = tBO.TeamName,
                };
                if (tBO.Players != null) result.Players = await Create(tBO.Players);
                else result.Players = new List<PlayerDto>();
                if (tBO.Sport != null) result.Sport = await Create(tBO.Sport);
                else result.Sport = new SportDto();
                teamsDto.Add(result);
            }
            return await Task.FromResult(teamsDto);
        }

        /// <summary>
        /// Create a Task with a Sport Dto
        /// </summary>
        /// <param name="poco">Sport Bussiness Object</param>
        /// <returns>Sport Dto</returns>
        public async Task<SportDto> Create(SportBO poco)
        {
            var result = new SportDto
            {
                Name = poco.Name,
                SportId = poco.SportId
            };
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a Position Dto
        /// </summary>
        /// <param name="poco">Position Bussiness Object</param>
        /// <returns>Position Dto</returns>
        public async Task<PositionDto> Create(PositionBO poco)
        {
            var result = new PositionDto
            {
                PositionId = poco.PositionId,
                PositionName = poco.PositionName
            };
            result.Sport = await Create(poco.Sport);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a Player Dto
        /// </summary>
        /// <param name="poco">Player Bussiness Object</param>
        /// <returns>Player Dto</returns>
        public async Task<PlayerDto> Create(PlayerBO poco)
        {
            var result = new PlayerDto
            {
                PlayerId = poco.PlayerId,
                FirstName = poco.FirstName,
                LastName = poco.LastName,
                Photo = poco.Photo,
                PreferredName = poco.PreferredName,
                Salary = poco.Salary
            };
            if (result.Team == null) result.Team = new TeamDto();
            else result.Team = await Create(poco.Team);
            if (result.Position == null) result.Position = new PositionDto();
            else result.Position = await Create(poco.Position); ;
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a list of Player Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Player Dto's</returns>
        public async Task<IEnumerable<PlayerDto>> Create(IEnumerable<PlayerBO> poco)
        {
            var result = new List<PlayerDto>();
            foreach (PlayerBO player in poco)
            {
                PlayerDto pDto = new PlayerDto()
                {
                    PlayerId = player.PlayerId,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Photo = player.Photo,
                    PreferredName = player.PreferredName,
                    Salary = player.Salary
                };
                if (player.Team != null)
                {
                    pDto.Team = new TeamDto()
                    {
                        TeamId = player.Team.TeamId,
                        TeamLogo = player.Team.TeamLogo,
                        TeamName = player.Team.TeamName,
                        Abbr = player.Team.Abbr,
                        Market = player.Team.Market,
                        Sport = new SportDto()
                        {
                            Name = player.Team.Sport.Name,
                            SportId = player.Team.Sport.SportId
                        }
                    };
                    List<PlayerDto> players = new List<PlayerDto>();
                    foreach (PlayerBO pBO in player.Team.Players)
                    {
                        PlayerDto pDtoList = new PlayerDto()
                        {
                            PlayerId = pBO.PlayerId,
                            FirstName = pBO.FirstName,
                            LastName = pBO.LastName,
                            Photo = pBO.Photo,
                            PreferredName = pBO.PreferredName,
                            Salary = pBO.Salary,
                            Team = pDto.Team,
                            Position = await Create(player.Position)
                        };
                        players.Add(pDtoList);
                    }
                    pDto.Team.Players = players;
                    List<LeagueDto> leagues = new List<LeagueDto>();
                    foreach (LeagueBO lBO in player.Team.Leagues)
                    {
                        LeagueDto lDtoList = new LeagueDto()
                        {
                            alias = lBO.alias,
                            id = lBO.id,
                            name = lBO.name
                        };
                        leagues.Add(lDtoList);
                    }
                    pDto.Team.Leagues = leagues;
                }
                else pDto.Team = new TeamDto();
                pDto.Position = await Create(player.Position);
                result.Add(pDto);
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a list of Notifications Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Notifications Dto's</returns>
        public async Task<IEnumerable<NotificationDto>> Create(IEnumerable<NotificationBO> poco)
        {
            var result = new List<NotificationDto>();
            foreach (NotificationBO notifBO in poco)
            {
                NotificationDto notifDTO = new NotificationDto()
                {
                    Active = notifBO.Active,
                    Content = notifBO.Content,
                    Link = notifBO.Link,
                    Name = notifBO.Name,
                    NotificationId = notifBO.NotificationId,
                    User = new UserDto()
                    {
                        Email = notifBO.User.Email,
                        Login = notifBO.User.Login,
                        Money = notifBO.User.Money,
                        Password = notifBO.User.Password,
                        Points = notifBO.User.Point,
                        _comment = ""
                    },
                    _comment = ""
                };
                result.Add(notifDTO);
            }
            return await Task.FromResult(result);
        }
        /// <summary>
        /// Create a Task with a list of Information Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Notifications Dto's</returns>
        public async Task<IEnumerable<InformationDto>> Create(IEnumerable<InformationBO> poco)
        {
            var result = new List<InformationDto>();
            foreach (InformationBO infoBO in poco)
            {
                InformationDto infofDTO = new InformationDto()
                {
                    Content = infoBO.Content,
                    FinalDate = infoBO.FinalDate,
                    InitialDate = infoBO.InitialDate,
                    InformationId = infoBO.InformationId,
                    Name = infoBO.Name,
                    _comment = ""
                };
                result.Add(infofDTO);
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a list of Information Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Notifications Dto's</returns>
        public async Task<IEnumerable<PromotionDto>> Create(IEnumerable<PromotionBO> poco)
        {
            var result = new List<PromotionDto>();
            foreach (PromotionBO promoBO in poco)
            {
                PromotionDto promoDTO = new PromotionDto()
                {
                    PromoId = promoBO.PromoId,
                    Name = promoBO.Name,
                    Content = promoBO.Content,
                    Code = promoBO.Code,
                    _comment = ""
                };
                result.Add(promoDTO);
            }
            return await Task.FromResult(result);
        }
        /// <summary>
        /// Create a Task with a list of Goals Dto's 
        /// </summary>
        /// <param name="poco">List of Goals Bussiness Object</param>
        /// <returns>List of Notifications Dto's</returns>
        public async Task<IEnumerable<GoalDto>> Create(IEnumerable<GoalBO> poco)
        {
            var result = new List<GoalDto>();
            foreach (GoalBO gBO in poco)
            {
                GoalDto goalDTO = new GoalDto()
                {
                    CompletionCount = gBO.CompletionCount,
                    GoalId = gBO.GoalId,
                    GoalLogo = gBO.GoalLogo,
                    Name = gBO.Name
                };
                result.Add(goalDTO);
            }
            return await Task.FromResult(result);
        }
        /// <summary>
        /// Create a Task with a list of News Dto's 
        /// </summary>
        /// <param name="poco">List of News Bussiness Object</param>
        /// <returns>List of News Dto's</returns>
        public async Task<IEnumerable<NewsDto>> Create(IEnumerable<NewsBO> poco)
        {
            var result = new List<NewsDto>();
            foreach (NewsBO nBO in poco)
            {
                NewsDto nDTO = new NewsDto()
                {
                    Content = nBO.Content,
                    Date = nBO.Date,
                    NewsId = nBO.NewsId,
                    Tittle = nBO.Tittle
                };
                result.Add(nDTO);
            }
            return await Task.FromResult(result);
        }
        /// <summary>
        /// Create a Task with a list of Game Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Game Dto's</returns>
        public async Task<GameDto> Create(GameBO poco)
        {
            GameDto gDto = new GameDto()
            {
                GameId = poco.GameId,
                Humidity = poco.Humidity,
                Scheduled = poco.Scheduled,
                Temperture = poco.Temperture,
            };
            if (poco.AwayTeam == null) gDto.AwayTeam = new TeamDto() { };
            else
            {
                gDto.AwayTeam = new TeamDto()
                {
                    TeamId = poco.AwayTeam.TeamId,
                    TeamLogo = poco.AwayTeam.TeamLogo,
                    TeamName = poco.AwayTeam.TeamName
                };
            }
            if (poco.HomeTeam == null) gDto.HomeTeam = new TeamDto() { };
            else
            {
                gDto.HomeTeam = new TeamDto()
                {
                    TeamId = poco.HomeTeam.TeamId,
                    TeamLogo = poco.HomeTeam.TeamLogo,
                    TeamName = poco.HomeTeam.TeamName
                };
            }
            if (poco.Venue == null) gDto.Venue = new VenueDto();
            else
            {
                gDto.Venue = new VenueDto()
                {
                    Country = poco.Venue.Country,
                    Name = poco.Venue.Name,
                    State = poco.Venue.State,
                    Surface = poco.Venue.Surface,
                    VenueId = poco.Venue.VenueId
                };
            }
            if (poco.ClimaCondition == null) gDto.ClimaCondition = new ClimaConditionsDto();
            else
            {
                gDto.ClimaCondition = new ClimaConditionsDto()
                {
                    ClimaId = poco.ClimaCondition.ClimaId,
                    Condition = poco.ClimaCondition.Condition
                };
            }
            return await Task.FromResult(gDto);
        }
    }
}
