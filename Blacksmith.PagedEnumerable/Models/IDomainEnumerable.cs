using System.Collections.Generic;

namespace Blacksmith.PagedEnumerable.Models
{
    public interface IDomainEnumerable<TOut, TOrder> : IEnumerable<TOut>
    {
        int TotalCount { get; }
        IPageSettings Page { get; }
        IOrderStack<TOrder> Order { get; }
    }
}
