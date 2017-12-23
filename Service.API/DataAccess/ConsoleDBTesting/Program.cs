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
            ContestResponse result = new ContestResponse()
            {
                Contests = dbContext.Contests.Include("ContestType").ToList()
            };
            //foreach (Contest c in result.Contests)
            //{
            //    dbContext.F
            //}
            Console.WriteLine("The end");
            Console.ReadLine();
        }
}
}
