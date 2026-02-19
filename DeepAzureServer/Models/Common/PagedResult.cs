namespace DeepAzureServer.Models.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResult() { }

        public PagedResult(IEnumerable<T> listResult, int totalCount, int pageNumber, int pageSize)
        {
            Items = listResult;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PagedResult<R> Map<R>(Func<T, R> selector)
        {
            var mappedItems = Items.Select(selector).ToList();
            return new PagedResult<R>(mappedItems, TotalCount, PageNumber, PageSize);
        }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
