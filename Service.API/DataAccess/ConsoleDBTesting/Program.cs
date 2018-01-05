using Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleDBTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            FantasyContext dbContext = new FantasyContext();
            var gms = dbContext.Games.ToList();
            var lineups = dbContext.LineUps.ToList();
            var liupPlayers = dbContext.PlayerLineups.ToList();
            var contestLineups = dbContext.ContestLineups.ToList();

            PlayersResponse plresult = new PlayersResponse
            {
                Players = dbContext.Players.ToList()
            };
            ContestResponse result = new ContestResponse()
            {
                Contests = dbContext.Contests.Include("ContestType").Include("ContestGame").ToList()
            };
            foreach (Contest c in result.Contests)
            {
                List<Game> games = new List<Game>();
                foreach (ContestGame cg in c.ContestGame)
                {
                    Game g = dbContext.Games.Include("Venue").Include("ClimaCondition").First(x => x.GameId == cg.GameId);
                    games.Add(g);
                }
            }

            Console.WriteLine("The end");
            Console.ReadLine();
        }
    }
}
