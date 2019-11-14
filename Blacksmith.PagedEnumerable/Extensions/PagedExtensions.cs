using Blacksmith.DomainQuery.Models;

namespace Blacksmith.PagedEnumerable.Extensions
{
    public static class PagedExtensions
    {
        public static IDomainQuery<TOut, TOrder> setPageSettings<TOut, TOrder>(
            this IDomainQuery<TOut, TOrder> items
            , int currentPage, int pageSize)
        {
            items.Page.Current = currentPage;
            items.Page.Size = pageSize;
            return items;
        }

        public static IDomainQuery<TOut, TOrder> addOrderSettings<TOut, TOrder>(
            this IDomainQuery<TOut, TOrder> items
            , TOrder order, OrderDirection direction)
        {
            items.Order.push(order, direction);
            return items;
        }

        public static IDomainQuery<TOut, TOrder> clearOrder<TOut, TOrder>(this IDomainQuery<TOut, TOrder> items)
        {
            items.Order.clear();
            return items;
        }
    }
}
