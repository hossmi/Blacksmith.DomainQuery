using Blacksmith.PagedEnumerable.Tests.Queries;

namespace Blacksmith.PagedEnumerable.Tests.Services
{
    public interface IUserService
    {
        IUserDetailsDomainQuery getUserDetails();
    }
}