using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class NotificationBO
    {
        public int NotificationId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public UserBO User { get; set; }

        public string Link { get; set; }

        public bool Active { get; set; }
    }
}
