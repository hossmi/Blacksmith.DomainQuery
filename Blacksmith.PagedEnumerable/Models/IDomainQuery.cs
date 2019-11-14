using System.Collections.Generic;

namespace Blacksmith.DomainQuery.Models
{
    public interface IDomainQuery<TOut, TOrder> : IEnumerable<TOut>
    {
        int TotalCount { get; }
        IPageSettings Page { get; }
        IOrderStack<TOrder> Order { get; }
    }
}
