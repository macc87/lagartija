using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class LineupBO
    {
        public long LineUpId { get; set; }
        public IEnumerable<PlayerBO> Players { get; set; }
        public UserBO User { get; set; }
    }
}
