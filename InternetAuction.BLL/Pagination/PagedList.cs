using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Pagination
{
    /// <summary>
    /// Represents a paged list.
    /// </summary>
    /// <typeparam name="T">List entity type.</typeparam>
    public class PagedList<T> : List<T>
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsCount { get; set; }

        /// <summary>
        /// Creates a new instance of the paged list.
        /// </summary>
        /// <param name="items">Source from which paged list is created.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">Current page.</param>
        /// <param name="itemsCount">Total count of the items.</param>
        public PagedList(IEnumerable<T> items, int pageSize, int currentPage, int itemsCount)
        {
            this.AddRange(items);

            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int) Math.Ceiling(itemsCount / (double) pageSize);
            ItemsCount = itemsCount;
        }

        /// <summary>
        /// Creates a new instance of the paged list.
        /// </summary>
        /// <param name="source">Source from which paged list is created.</param>
        /// <param name="pageNumber">Current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static PagedList<T> CreateAsync(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PagedList<T>(items, pageSize, pageNumber, count);
        }
    }
}