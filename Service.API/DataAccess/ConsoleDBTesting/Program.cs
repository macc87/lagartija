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
            ContestResponse result = new ContestResponse()
            {
                Contests = dbContext.Contests.Include("ContestType").Include("ContestGame").ToList()
            };
            foreach (Contest c in result.Contests)
            {
               // c.Games = new List<Game>();
                foreach (ContestGame cg in c.ContestGame)
                {
                    Game g = dbContext.Games.Include("Venue").Include("AwayTeam").Include("HomeTeam").Include("ClimaCondition").First(x => x.GameId == cg.GameId);
                    //
                   // c.Games.Add(g);
                }
            }
            Console.WriteLine("The end");
            Console.ReadLine();
        }
    }
}
