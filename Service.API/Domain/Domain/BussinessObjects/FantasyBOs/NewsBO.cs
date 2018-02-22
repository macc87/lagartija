using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class NewsBO
    {
        public long NewsId { get; set; }

        public string Tittle { get; set; }

        public string Content { get; set; }

        public System.DateTime Date { get; set; }
    }
}
