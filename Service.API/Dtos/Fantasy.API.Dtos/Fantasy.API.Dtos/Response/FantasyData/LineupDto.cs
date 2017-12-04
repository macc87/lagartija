﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    class LineupDto
    {
        public int LineUpId { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
        public UserDto User { get; set; }
    }
}
