using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class VenueBO
    {
        public int VenueId { get; set; }

        public string Name { get; set; }

        public string Surface { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
