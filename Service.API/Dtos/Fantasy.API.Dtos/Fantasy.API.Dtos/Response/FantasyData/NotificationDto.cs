using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Dtos.Response.FantasyData
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public bool Active { get; set; }

        public UserDto User { get; set; }

        public string _comment { get; set; }

        public override string ToString()
        {
            return "NotificationDto";
        }
    }
}
