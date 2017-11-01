using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Service.Mapping.RequestMapping
{
    public class DtoFactoryRequest
    {
        public async Task<string> Create(string acronym)
        {
            return await Task.FromResult(acronym);
        }
    }
}
