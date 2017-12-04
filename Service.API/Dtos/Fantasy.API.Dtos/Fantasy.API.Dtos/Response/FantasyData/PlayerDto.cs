using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    class PlayerDto
    {
        public int PlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PreferredName { get; set; }

        public TeamDto Team { get; set; }
        
        public PositionDto Position { get; set; }
        
        public double Salary { get; set; }
       
        public string Photo { get; set; }
    }
}
