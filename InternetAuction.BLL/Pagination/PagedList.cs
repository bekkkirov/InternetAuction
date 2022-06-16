using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsCount { get; set; }

        public PagedList(IEnumerable<T> items, int pageSize, int currentPage, int itemsCount)
        {
            this.AddRange(items);

            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int) Math.Ceiling(itemsCount / (double) pageSize);
            ItemsCount = itemsCount;
        }

        public static PagedList<T> CreateAsync(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PagedList<T>(items, pageSize, pageNumber, count);
        }
    }
}