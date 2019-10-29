using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.PagedEnumerable.Extensions;
using Blacksmith.PagedEnumerable.Tests.Contexts;
using Blacksmith.PagedEnumerable.Tests.DomainQueries;
using Blacksmith.PagedEnumerable.Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blacksmith.PagedEnumerable.Tests
{
    public class UnitTest1
    {
        private readonly TestContext context;
        private readonly IDomainEnumerableStrings strings;

        public UnitTest1()
        {
            this.context = prv_getContext();
            this.strings = new EnDomainEnumerableStrings();
        }

        [Fact]
        public void mapped_pagination()
        {
            IDomainEnumerable<UserDetails, UserDetailsColumns> usersQuery;
            IList<UserDetails> listedUsers;

            usersQuery = prv_getUserDetails(this.context, this.strings);
            
            Assert.Equal(4, usersQuery.TotalCount);
            Assert.Equal(4, usersQuery.Count());

            usersQuery
                .addOrderSettings(UserDetailsColumns.RoleName, OrderDirection.Descending)
                .addOrderSettings(UserDetailsColumns.UserName, OrderDirection.Ascending);

            listedUsers = usersQuery.ToList();
            Assert.Equal(4, listedUsers.Count);

            Assert.Equal("Reader", listedUsers[0].RoleName);
            Assert.Equal("Narciso", listedUsers[0].UserName);

            Assert.Equal("Admin", listedUsers[3].RoleName);
            Assert.Equal("Root", listedUsers[3].UserName);
        }

        private static IDomainEnumerable<UserDetails, UserDetailsColumns> prv_getUserDetails(TestContext context, IDomainEnumerableStrings strings)
        {
            return new UserDetailsQuery(context.UserRoles, strings);
        }

        private static TestContext prv_getContext()
        {
            TestContext context;
            DbContextOptions<TestContext> options;

            options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase("in memory")
                .Options;

            context = new TestContext(options);
            
            prv_seedContext(context);

            return context;
        }

        private static void prv_seedContext(TestContext context)
        {
            if(false == context.Users.Any())
            {
                User narcisoUser;
                Role adminRole, editorRole, readerRole;

                narcisoUser = new User { Name = "Narciso" };

                context.Users.Add(narcisoUser);
                context.Users.Add(new User { Name = "Florencio" });
                context.Users.Add(new User { Name = "Rosa" });
                context.Users.Add(new User { Name = "Pepe" });
                context.Users.Add(new User { Name = "Tonco" });
                context.SaveChanges();

                adminRole = new Role { Name = "Admin" };
                context.Roles.Add(adminRole);
                editorRole = new Role { Name = "Editor" };
                context.Roles.Add(editorRole);
                readerRole = new Role { Name = "Reader" };
                context.Roles.Add(readerRole);
                context.SaveChanges();

                context.UserRoles.Add(new UserRole
                {
                    RoleId = readerRole.Id,
                    UserId = narcisoUser.Id,
                });

                context.UserRoles.Add(new UserRole
                {
                    RoleId = editorRole.Id,
                    UserId = narcisoUser.Id,
                });

                context.UserRoles.Add(new UserRole
                {
                    RoleId = adminRole.Id,
                    UserId = narcisoUser.Id,
                });

                context.UserRoles.Add(new UserRole
                {
                    RoleId = adminRole.Id,
                    User = new User { Name = "Root" },
                });
                context.SaveChanges();
            }
        }
    }
}
