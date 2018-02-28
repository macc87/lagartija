using System.Collections.Generic;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class InformationDto
    {

        public string _comment { get; set; }

        public long InformationId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public System.DateTime InitialDate { get; set; }

        public System.DateTime FinalDate { get; set; }
    }
}
