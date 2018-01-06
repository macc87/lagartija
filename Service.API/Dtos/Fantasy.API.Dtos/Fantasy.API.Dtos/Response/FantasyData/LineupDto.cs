using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class LineupDto
    {
        public long LineUpId { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public UserDto User { get; set; }
        public string _comment { get; set; }
    }
}
