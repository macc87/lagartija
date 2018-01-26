using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    public class ContestResponse
    {
        public Contest Contest { get; set; }
        public List<Game> Games { get; set; }
        public List<LineUp> Lineups { get; set; }
        public DateTime Starts { get; set; }
        public string _comment { get; set; }
    }
}
