using Assurant.ASP.Api.Domain.Mapping;
using Fantasy.API.DataAccess.Services.Fantasy;
using Fantasy.API.DataAccess.Services.Fantasy.Interfase;
using Fantasy.API.Domain.BussinessObjects.FantasyBOs;
using Fantasy.API.Utilities.ServicesHandler;
using Fantasy.API.Utilities.ServicesHandler.Core;
using System;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.Services.FantasyService.Core
{
    public class FantasyServiceCore : BaseService, IDisposable
    {
        private bool _disposed;
        private readonly IFantasyClient FantasyClient = new SportsRadarClient();

        internal async Task<ServiceResult<InjuriesBO>> GetInjuriesAsync()
        {
            try
            {
                var result = await FantasyClient.GetInjuriesAsync();
                if (result == null)
                {
                    throw new ServiceException(message: "Unable to get injured players");
                }
                if (result.HasError)
                {
                    throw new ServiceException(message: result.Messages.Description, httpStatusCode: result.HttpStatusCode, exception: result.InnerException);
                }
                var resultMapping = await ModelFactories.ModelFactoryFantasy.Create(result.Result);
                return await ServiceOkAsync(resultMapping);
            }
            catch (Exception exception)
            {
                return ExceptionHandler<InjuriesBO>(exception);
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
                    FantasyClient.Dispose();
                }
            }
            _disposed = true;
        }

        ~FantasyServiceCore()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
