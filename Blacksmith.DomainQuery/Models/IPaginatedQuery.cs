using System.Collections.Generic;

namespace Blacksmith.DomainQuery.Models
{
    public interface IPaginatedQuery<TOut> : IEnumerable<TOut>
    {
        int TotalCount { get; }
        IPageSettings Page { get; }
    }
}
