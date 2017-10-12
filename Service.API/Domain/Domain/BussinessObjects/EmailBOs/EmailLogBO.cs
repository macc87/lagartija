using System;
using System.Collections.Generic;

namespace Assurant.ASP.Api.Domain.BussinessObjects.EmailBOs
{
    public class EmailLogBO
    {
        public EmailLogBO()
        {
            Roles = new List<string>();
        }
        public DateTime Date { get; set; }
        public string UserEmail { get; set; }
        public string CompanyName { get; set; }
        public string ApplicationName { get; set; }
        public List<string> Roles { get; set; }
        public string SubjectLine { get; set; }
        public string Body { get; set; }
        public string FromRecipient { get; set; }
        public string ToRecipient { get; set; }
    }
}
