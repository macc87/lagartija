using System.Collections.Generic;

namespace Assurant.ASP.Api.Dtos.CMC.ModelClient
{
    public interface IPaginatedDto<TDto>
    {

        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int TotalPageCount { get; set; }

        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }

         List<TDto> Items { get; }
    }

}