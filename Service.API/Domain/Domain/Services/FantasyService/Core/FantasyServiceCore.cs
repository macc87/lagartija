using System;
using System.Threading.Tasks;
using Domain.BussinessObjects.FantasyBOs;
using Utilities.ServicesHandler.Core;
using Utilities.ServicesHandler;
using DataAccess.UnitOfWork;
using Utilities.Validation;
using DataAccess.Repository;
using DataAccess.Models.Claim.Services.FantasyData;
using DataAccess.Models.Claim.MSSQL.Fantasy;
using System.Linq;
using System.Data.Entity;

namespace Domain.Services.FantasyService.Core
{
    public class FantasyServiceCore : BaseService, IDisposable
    {
        private bool _disposed;
        private UserInfo _userInfo;
        private IUnitOfWork _unitOfWorkSql;
        internal readonly IRepository<InjuryPlayer> InjuriesRepository;

        public FantasyServiceCore(IUnitOfWork unitOfWorkSql, UserInfo userInfo)
        {
            Check.NotNull(unitOfWorkSql, "unitOfWorkMsSql");
            InjuriesRepository = new Repository<InjuryPlayer>(unitOfWorkSql);
            _unitOfWorkSql = unitOfWorkSql;
            _userInfo = userInfo;
        }

        internal async Task<ServiceResult<InjuriesBO>> GetInjuriesAsync()
        {
            try
            {
                var injuries = _unitOfWorkSql.Context.Set<InjuryPlayer>()
                             .Include(x => x.Injury)
                             .Include(x => x.InjuryTeam);
                             


                if (injuries == null)
                    throw new Exception("No injuries found");
                var result = new InjuriesBO();

                return await ServiceOkAsync(result);
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
                    InjuriesRepository.Dispose();
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
