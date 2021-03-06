﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class ContestDto
    {
        public long ContestId { get; set; }

        public ContestTypeDto ContestTypeId { get; set; }

        public string Name { get; set; }

        public int SignedUp { get; set; }

        public int MaxCapacity { get; set; }

        public double EntryFee { get; set; }

        public double SalaryCap { get; set; }

        public DateTime Starts { get; set; }

        public IEnumerable<GameDto> Games { get; set;}
        public IEnumerable<LineupDto> LineUpsDto { get; set; }
    }
}
