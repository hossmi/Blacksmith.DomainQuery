using Blacksmith.DomainQuery.Models;
using Blacksmith.PagedEnumerable.Tests.Contexts;
using Blacksmith.PagedEnumerable.Tests.Models;
using Blacksmith.PagedEnumerable.Tests.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Blacksmith.PagedEnumerable.Tests.Services
{
    public class UserService : IUserService
    {
        private readonly TestContext context;

        public UserService(TestContext context)
        {
            this.context = context;
        }

        public IUserDetailsDomainQuery getUserDetails()
        {
            return new PrvUserDetailsDomainQuery(this.context.UserRoles);
        }

        private class PrvUserDetailsDomainQuery : AbstractDomainQuery<UserRole, UserDetails, UserDetailsColumns>, IUserDetailsDomainQuery
        {
            public PrvUserDetailsDomainQuery(IQueryable<UserRole> userRoles) : base(userRoles) { }

            protected override UserDetails mapToDomain(UserRole item)
            {
                return new UserDetails
                {
                    UserName = item.User.Name,
                    RoleName = item.Role.Name,
                    Active = item.Active,
                };
            }

            protected override Expression<Func<UserRole, object>> getKeySelector(UserDetailsColumns key)
            {
                switch (key)
                {
                    case UserDetailsColumns.UserName:
                        return ur => ur.User.Name;
                    case UserDetailsColumns.Priority:
                        return ur => ur.Role.Priority;
                    case UserDetailsColumns.Active:
                        return ur => ur.Active;
                    default:
                        throw new ArgumentException("Invalid column", nameof(key));
                }
            }
        }
    }
}