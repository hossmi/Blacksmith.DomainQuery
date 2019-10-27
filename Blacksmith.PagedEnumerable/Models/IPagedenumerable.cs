using System.Collections.Generic;

namespace Blacksmith.PagedEnumerable.Models
{
    public interface IPagedEnumerable<T> : IEnumerable<T>
    {
        int TotalCount { get; }
        IPageSettings Page { get; }
    }
}
