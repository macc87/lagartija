using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Service.Mapping.RequestMapping
{
    /// <summary>
    /// 
    /// </summary>
    public class DtoFactoryRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acronym"></param>
        /// <returns></returns>
        public async Task<string> Create(string acronym)
        {
            return await Task.FromResult(acronym);
        }
    }
}
