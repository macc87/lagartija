﻿using Fantasy.API.DataAccess.Models.Services.FantasyData;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fantasy.API.Utilities.ServicesHandler.Core;

namespace Fantasy.API.DataAccess.Services.Fantasy.Interfase
{
    public interface IFantasyDataClient : IDisposable
    {
        Task<ServiceResult<ContestResponse>> GetContestsAsync();
    }
}