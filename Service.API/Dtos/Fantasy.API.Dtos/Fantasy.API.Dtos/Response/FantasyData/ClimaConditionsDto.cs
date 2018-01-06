using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class ClimaConditionsDto
    {
        public long ClimaId { get; set; }

        public string Condition { get; set; }
        public string _comment { get; set; }
    }
}
