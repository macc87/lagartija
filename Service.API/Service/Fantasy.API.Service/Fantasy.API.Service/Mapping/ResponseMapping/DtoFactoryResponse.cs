﻿using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Dtos.Response.FantasyData;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        /// <summary>
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

        /// <summary>
        /// Create a Task with a League Dto 
        /// </summary>
        /// <param name="league">League Bussiness Object</param>
        /// <returns>Contest Dto</returns>
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
                gDto.AwayTeam = await Create(game.AwayTeam);
                gDto.HomeTeam = await Create(game.HomeTeam);
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
            return await Task.FromResult(result);
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
            result.Team = await Create(poco.Team);
            result.Position = await Create(poco.Position);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Create a Task with a list of Games Dto's 
        /// </summary>
        /// <param name="poco">List of Game Bussiness Object</param>
        /// <returns>List of Games Dto's</returns>
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
                pDto.Team = await Create(player.Team);
                pDto.Position = await Create(player.Position);
                result.Add(pDto);
            }
            return await Task.FromResult(result);
        }
    }
}
