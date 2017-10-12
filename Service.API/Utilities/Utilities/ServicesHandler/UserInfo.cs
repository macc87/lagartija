using System.Collections.Generic;

namespace Utilities.ServicesHandler
{
    public class UserInfo
    {
        public string UserSystemId;
        public string Id;
        public string Guid;
        public string Email { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationId { get; set; }
        public List<string> Role { get; set; }
    }
}