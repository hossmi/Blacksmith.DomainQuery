using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.PagedEnumerable.Tests.Contexts;
using Blacksmith.PagedEnumerable.Tests.Models;
using System.Linq;

namespace Blacksmith.PagedEnumerable.Tests.DomainQueries
{
    public class UserDetailsQuery : AbstractDomainEnumerable<UserRole, UserDetails, UserDetailsColumns>
    {
        public UserDetailsQuery(IQueryable<UserRole> userRoles, IDomainEnumerableStrings strings) : base(userRoles, strings)
        {
        }

        protected override UserDetails prv_mapToDomain(UserRole item)
        {
            return new UserDetails
            {
                UserName = item.User.Name,
                RoleName = item.Role.Name,
                Active = item.Active,
            };
        }

        protected override IOrderedQueryable<UserRole> prv_setFirstOrder(IQueryable<UserRole> query
            , UserDetailsColumns key, OrderDirection direction)
        {
            this.assert.isValidEnum(key);

            switch (key)
            {
                case UserDetailsColumns.UserName:
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(i => i.User.Name)
                        : query.OrderByDescending(i => i.User.Name);
                case UserDetailsColumns.RoleName:
                    return direction == OrderDirection.Ascending
                        ? query.OrderBy(i => i.Role.Name)
                        : query.OrderByDescending(i => i.Role.Name);
                case UserDetailsColumns.Active:
                    return direction == OrderDirection.Ascending 
                        ? query.OrderBy(i => i.Active)
                        : query.OrderByDescending(i => i.Active);
                default:
                    return null;
            }
        }

        protected override IOrderedQueryable<UserRole> prv_setNextOrder(IOrderedQueryable<UserRole> query
            , UserDetailsColumns key, OrderDirection direction)
        {
            this.assert.isValidEnum(key);

            switch (key)
            {
                case UserDetailsColumns.UserName:
                    return direction == OrderDirection.Ascending
                        ? query.ThenBy(i => i.User.Name)
                        : query.ThenByDescending(i => i.User.Name);
                case UserDetailsColumns.RoleName:
                    return direction == OrderDirection.Ascending
                        ? query.ThenBy(i => i.Role.Name)
                        : query.ThenByDescending(i => i.Role.Name);
                case UserDetailsColumns.Active:
                    return direction == OrderDirection.Ascending
                        ? query.ThenBy(i => i.Active)
                        : query.ThenByDescending(i => i.Active);
                default:
                    return null;
            }
        }
    }
}