using Blacksmith.PagedEnumerable.Models;
using Blacksmith.PagedEnumerable.Models.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blacksmith.PagedEnumerable.Extensions
{
    public static class PagedExtensions
    {
        public static IPagedEnumerable<T> paginate<T>(this IQueryable<T> items)
        {
            return new DefaultPagedEnumerable<T>(items, new Validations.);
        }

        public static IPagedEnumerable<T> setCurrentPage<T>(this IPagedEnumerable<T> items, int page)
        {
            items.Page.Current = page;
            return items;
        }

        public static ISortedPagedEnumerable<T, TKey> setCurrentPage<T, TKey>(this ISortedPagedEnumerable<T,TKey> items, int page)
        {
            items.Page.Current = page;
            return items;
        }

        public static IPagedEnumerable<T> setPageSize<T>(this IPagedEnumerable<T> items, int pageSize)
        {
            items.Page.Size = pageSize;
            return items;
        }

        public static ISortedPagedEnumerable<T, TKey> setPageSize<T, TKey>(this ISortedPagedEnumerable<T, TKey> items, int pageSize)
        {
            items.Page.Size = pageSize;
            return items;
        }
    }
}
