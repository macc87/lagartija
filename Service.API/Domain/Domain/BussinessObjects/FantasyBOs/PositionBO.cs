using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class PositionBO
    {
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public SportBO Sport { get; set; }
    }
}

