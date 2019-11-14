namespace Blacksmith.DomainQuery.Models
{
    public interface IOrderStack<TKey>
    {
        int Count { get; }

        void push(TKey field, OrderDirection direction);
        void pop();
        void clear();
    }
}