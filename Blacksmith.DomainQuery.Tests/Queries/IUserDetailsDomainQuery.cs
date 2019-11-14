using Blacksmith.DomainQuery.Models;
using Blacksmith.PagedEnumerable.Tests.Models;

namespace Blacksmith.PagedEnumerable.Tests.Queries
{
    public interface IUserDetailsDomainQuery : IDomainQuery<UserDetails, UserDetailsColumns>
    {
    }
}