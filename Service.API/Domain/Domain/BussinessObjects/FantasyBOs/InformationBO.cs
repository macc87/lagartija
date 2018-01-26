using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class InformationBO
    {
        public long InformationId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public System.DateTime InitialDate { get; set; }
        public System.DateTime FinalDate { get; set; }
    }
}
