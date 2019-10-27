using System.Collections.Generic;

namespace Blacksmith.PagedEnumerable.Models
{
    public interface IPagedEnumerable<T, TKey> : IEnumerable<T>
    {
        int TotalCount { get; }
        IPageSettings Page { get; }
        IOrderSettings<TKey> Order { get; }
    }
}
