﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    class PlayerBO
    {
        public int PlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PreferredName { get; set; }

        public TeamBO Team { get; set; }
        
        public PositionBO Position { get; set; }
        
        public double Salary { get; set; }
       
        public string Photo { get; set; }
    }
}