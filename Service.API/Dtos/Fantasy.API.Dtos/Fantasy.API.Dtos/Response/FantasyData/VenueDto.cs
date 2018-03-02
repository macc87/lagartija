using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class VenueDto
    {
        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Surface { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
        public string _comment { get; set; }
        
    }
}
