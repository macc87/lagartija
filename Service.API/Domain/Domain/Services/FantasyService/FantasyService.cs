using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Domain.Services.FantasyService.Core;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using Fantasy.API.Utilities.Validation;
using System;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyService
{
    public class FantasyService : IFantasyService
    {
        private bool _disposed;
        private readonly FantasyServiceCore _fantasyServiceCore;

        public FantasyService(IFantasyClient client)
        {
            Check.NotNull(client, "client");
            _fantasyServiceCore = new FantasyServiceCore(client);
        }

        public async Task<ServiceResult<InjuriesBO>> GetInjuriesAsync()
        {
            try
            {
                return await _fantasyServiceCore.GetInjuriesAsync();
            }
            catch (Exception exception)
            {
                return _fantasyServiceCore.ExceptionHandler<InjuriesBO>(exception);

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
                    _fantasyServiceCore.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyService()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
