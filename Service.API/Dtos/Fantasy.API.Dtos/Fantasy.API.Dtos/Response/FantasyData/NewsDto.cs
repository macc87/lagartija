using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class NewsDto
    {
        public long NewsId { get; set; }

        public string Tittle { get; set; }

        public string Content { get; set; }

        public System.DateTime Date { get; set; }

        public string _comment { get; set; }
    }
}
