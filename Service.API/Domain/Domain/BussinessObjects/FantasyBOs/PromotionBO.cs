﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    class PromotionBO
    {
        public int PromoId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Code { get; set; }
    }
}