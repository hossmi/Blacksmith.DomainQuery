using Blacksmith.PagedEnumerable.Models;

namespace Blacksmith.PagedEnumerable.Extensions
{
    public static class PagedExtensions
    {
        public static IDomainEnumerable<TOut, TOrder> setPageSettings<TOut, TOrder>(
            this IDomainEnumerable<TOut, TOrder> items
            , int currentPage, int pageSize)
        {
            items.Page.Current = currentPage;
            items.Page.Size = pageSize;
            return items;
        }

        public static IDomainEnumerable<TOut, TOrder> addOrderSettings<TOut, TOrder>(
            this IDomainEnumerable<TOut, TOrder> items
            , TOrder order, OrderDirection direction)
        {
            items.Order.push(order, direction);
            return items;
        }

        public static IDomainEnumerable<TOut, TOrder> clearOrder<TOut, TOrder>(this IDomainEnumerable<TOut, TOrder> items)
        {
            items.Order.clear();
            return items;
        }
    }
}
