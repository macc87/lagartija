using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Models.MSSQL.Fantasy
{
    public class InformationsResponse
    {
        public List<Information> Informations { get; set; }
        public string _comment { get; set; }
    }
}
