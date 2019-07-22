using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.Core
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public decimal TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;


        public PageList(List<T> items,int total, FilterOptions filterOptions = null)
        {
            CurrentPage = filterOptions.CurrentPage;
            PageSize = filterOptions.PageSize;
            TotalCount = total;
            TotalPages = Math.Ceiling((decimal)total / PageSize);
            AddRange(items);
        }
    }
}
