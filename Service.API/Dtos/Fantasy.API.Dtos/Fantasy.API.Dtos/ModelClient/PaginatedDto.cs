using System.Collections.Generic;

namespace Assurant.ASP.Api.Dtos.CMC.ModelClient
{

    public class PaginatedDto<TDto> : IPaginatedDto<TDto>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public List<TDto> Items { get; set; }
    }
}