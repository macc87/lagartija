using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    class PositionDto
    {
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public SportDto Sport { get; set; }
    }
}

