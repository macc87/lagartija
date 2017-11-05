using System;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public interface IFantasyService : IDisposable
    {
        Task<ServiceResult<InjuriesBO>> GetInjuriesAsync();
    }
}
