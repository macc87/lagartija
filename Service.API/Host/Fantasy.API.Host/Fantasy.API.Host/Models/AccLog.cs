using System;

namespace Fantasy.API.Host.Models
{
    public class AccLog
    {
        public String UserId { get; set; }
        public String Email { get; set; }
        public String RequestUrl { get; set; }
        public String ErrorMessage { get; set; }
        public String Origin { get; set; }
        public String ErrorCode { get; set; }
        public String ErrorType { get; set; }
        public String ComputerName { get; set; }
        public DateTime Date { get; set; }
    }
}