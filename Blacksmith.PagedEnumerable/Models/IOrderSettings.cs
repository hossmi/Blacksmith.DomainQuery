namespace Blacksmith.PagedEnumerable.Models
{
    public interface IOrderSettings<TKey>
    {
        void pushOrder(TKey key, OrderDirection direction);
        bool popOrder();
        void clear();
    }
}