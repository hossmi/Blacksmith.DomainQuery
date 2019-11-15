using System.Collections.Generic;

namespace Blacksmith.DomainQuery.Models
{
    public interface IOrderStack<TKey> : IEnumerable<KeyValuePair<TKey, OrderDirection>>
    {
        int Count { get; }

        void push(TKey field, OrderDirection direction);
        void pop();
        void clear();
    }
}