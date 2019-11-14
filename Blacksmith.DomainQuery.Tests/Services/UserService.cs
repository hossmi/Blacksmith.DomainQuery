using Blacksmith.DomainQuery.Models;
using Blacksmith.PagedEnumerable.Tests.Contexts;
using Blacksmith.PagedEnumerable.Tests.Models;
using Blacksmith.PagedEnumerable.Tests.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

            protected override IOrderedQueryable<UserRole> setFirstOrder(
                IQueryable<UserRole> query, UserDetailsColumns key, OrderDirection direction)
            {
                switch (key)
                {
                    case UserDetailsColumns.UserName:
                        return query.orderBy(ur => ur.User.Name, direction);
                    case UserDetailsColumns.Priority:
                        return query.orderBy(ur => ur.Role.Priority, direction);
                    case UserDetailsColumns.Active:
                        return query.orderBy(ur => ur.Active, direction);
                    default:
                        throw new ArgumentException("Invalid column", nameof(key));
                }
            }

            protected override IOrderedQueryable<UserRole> setNextOrder(
                IOrderedQueryable<UserRole> query, UserDetailsColumns key, OrderDirection direction)
            {
                switch (key)
                {
                    case UserDetailsColumns.UserName:
                        return query.thenBy(ur => ur.User.Name, direction);
                    case UserDetailsColumns.Priority:
                        return query.thenBy(ur => ur.Role.Priority, direction);
                    case UserDetailsColumns.Active:
                        return query.thenBy(ur => ur.Active, direction);
                    default:
                        throw new ArgumentException("Invalid column", nameof(key));
                }
            }
        }
    }
}