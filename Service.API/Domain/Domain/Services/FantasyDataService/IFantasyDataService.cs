using System;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using System.Collections.Generic;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public interface IFantasyDataService : IDisposable
    {
        Task<ServiceResult<List<ContestBO>>> GetContestsAsync();
    }
}
