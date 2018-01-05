using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class GameBO
    {
        public long GameId { get; set; }

        public System.DateTime Scheduled { get; set; }

        public double Humidity { get; set; }

        public double Temperture { get; set; }

        public VenueBO Venue { get; set; }

        public TeamBO AwayTeam { get; set; }
        public TeamBO HomeTeam { get; set; }

        public ClimaConditionsBO ClimaCondition { get; set; }
    }
}
