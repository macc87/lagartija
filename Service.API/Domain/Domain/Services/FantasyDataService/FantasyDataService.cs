using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Domain.Services.FantasyDataService.Core;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public class FantasyDataService : IFantasyDataService
    {
        private bool _disposed;
        private readonly FantasyDataServiceCore _fantasyDataServiceCore;

        public FantasyDataService(IFantasyDataClient client)
        {
            Check.NotNull(client, "client");
            _fantasyDataServiceCore = new FantasyDataServiceCore(client);
        }

        public async Task<ServiceResult<List<ContestBO>>> GetContestsAsync()
        {
            try
            {
                return await _fantasyDataServiceCore.GetContestsAsync();
            }
            catch (Exception exception)
            {
                return _fantasyDataServiceCore.ExceptionHandler<List<ContestBO>>(exception);

            }
        }

        #region [Disposing]

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fantasyDataServiceCore.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyDataService()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
