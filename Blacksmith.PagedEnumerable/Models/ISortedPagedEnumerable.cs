using System.Collections.Generic;

namespace Blacksmith.PagedEnumerable.Models
{
    public interface ISortedPagedEnumerable<T, TKey> : IPagedEnumerable<T>
    {
        IOrderSettings<TKey> Order { get; }
    }
}
