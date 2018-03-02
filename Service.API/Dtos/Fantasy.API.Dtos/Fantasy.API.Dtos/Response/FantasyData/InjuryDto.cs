using System.Collections.Generic;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class InjuryDto
    {

        public string _comment { get; set; }

        public long InjuryId { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public System.DateTime StartDate { get; set; }

        public System.DateTime UpdateDate { get; set; }

        public PlayerDto Player { get; set; }
    }
}
