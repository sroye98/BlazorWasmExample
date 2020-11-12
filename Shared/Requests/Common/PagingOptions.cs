using System;

namespace Shared.Requests.Common
{
    public class PagingOptions
    {
        public PagingOptions()
        {
        }

        public bool DescendingOrder { get; set; } = true;

        public int PageSize { get; set; } = 25;

        public string[] Projections { get; set; } = new string[] { };

        public string SearchQuery { get; set; }

        public int Skip { get; set; } = 0;

        public string SortColumn { get; set; }
    }
}
