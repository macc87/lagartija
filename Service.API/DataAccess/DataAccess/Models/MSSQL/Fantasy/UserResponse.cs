using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    public class UserResponse
    {
        public Account User { get; set; }
        public double Money { get; set; }
        public double Point { get; set; }
        public List<Account> Friends { get; set; }

        public string _comment { get; set; }
    }
}
