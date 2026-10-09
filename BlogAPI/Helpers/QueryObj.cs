using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Helpers
{
    public class QueryObj
    {
        public int? CategoryId { get; set; }

        public string? AuthorName {get; set; } = string.Empty;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}