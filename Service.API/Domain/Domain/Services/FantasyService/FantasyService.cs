using Domain.Services.FantasyService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BussinessObjects.FantasyBOs;
using Utilities.ServicesHandler.Core;
using Utilities.ServicesHandler;
using DataAccess.UnitOfWork;
using Utilities.Validation;

namespace Domain.Services.FantasyService
{
    public class FantasyService : IFantasyService
    {
        private bool _disposed;
        private readonly FantasyServiceCore _fantasyServiceCore;

        public FantasyService(IUnitOfWork unitOfWorkSql, UserInfo userInfo)
        {
            Check.NotNull(unitOfWorkSql, "unitOfWorkSql");
            Check.NotNull(userInfo, "userInfo");
            _fantasyServiceCore = new FantasyServiceCore(unitOfWorkSql, userInfo);
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
