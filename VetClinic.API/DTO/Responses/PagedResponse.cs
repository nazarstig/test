using System.Collections.Generic;
using VetClinic.API.DTO.Queries;

namespace VetClinic.API.DTO.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data, PaginationQuery paginationQuery)
        {
            Data = data;
            PageNumber = paginationQuery.PageNumber;
            PageSize = paginationQuery.PageSize;
        }

        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}