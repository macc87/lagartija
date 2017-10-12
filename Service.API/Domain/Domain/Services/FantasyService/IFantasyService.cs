using Domain.BussinessObjects.FantasyBOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ServicesHandler.Core;

namespace Domain.Services.FantasyService
{
    public interface IFantasyService : IDisposable
    {
        Task<ServiceResult<InjuriesBO>> GetInjuriesAsync();
    }
}
