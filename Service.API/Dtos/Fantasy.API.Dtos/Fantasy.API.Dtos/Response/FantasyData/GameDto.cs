using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    class GameDto
    {
        public int GameId { get; set; }

        public System.DateTime Scheduled { get; set; }

        public double Humidity { get; set; }

        public double Temperture { get; set; }

        public VenueDto Venue { get; set; }

        public TeamDto AwayTeam { get; set; }
        public TeamDto HomeTeam { get; set; }

        public ClimaConditionsDto ClimaCondition { get; set; }
    }
}
