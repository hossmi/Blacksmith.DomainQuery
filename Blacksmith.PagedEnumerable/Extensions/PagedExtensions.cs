using Blacksmith.PagedEnumerable.Exceptions;
using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.PagedEnumerable.Models.Internals;
using Blacksmith.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blacksmith.PagedEnumerable.Extensions
{
    public static class PagedExtensions
    {
        private static readonly IPagedEnumerableStrings strings;
        private static readonly IValidator validator;

        static PagedExtensions()
        {
            strings = new EnPagedEnumerableStrings();
            validator = new Validator<PagedEnumerableException>(strings, prv_buildException);
        }

        public static IPagedEnumerable<Tout, TKey> paginate<Tin, Tout, Tkey>(this IQueryable<T> items, Func<Tin, Tout> mapper)
        {
            return new DefaultPagedEnumerable<T>(items, validator, strings);
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
