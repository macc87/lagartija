using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class PromotionDto
    {
        public long PromoId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Code { get; set; }
        public string _comment { get; set; }
    }
}
