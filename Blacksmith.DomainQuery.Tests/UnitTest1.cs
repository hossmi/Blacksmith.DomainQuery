using Blacksmith.PagedEnumerable.Extensions;
using Blacksmith.PagedEnumerable.Tests.Contexts;
using Blacksmith.PagedEnumerable.Tests.Models;
using Blacksmith.PagedEnumerable.Tests.Queries;
using Blacksmith.PagedEnumerable.Tests.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blacksmith.PagedEnumerable.Tests
{
    public class UnitTest1
    {
        private readonly TestContext context;
        private readonly IUserService userService;

        public UnitTest1()
        {
            this.context = prv_getContext();
            this.userService = new UserService(this.context);
        }

        [Fact]
        public void mapped_pagination()
        {
            IUserDetailsDomainQuery users;
            IList<UserDetails> listedUsers;

            users = this.userService.getUserDetails();

            Assert.Equal(4, users.TotalCount);
            Assert.Equal(4, users.Count());

            users
                .addOrderSettings(UserDetailsColumns.Priority, OrderDirection.Descendant)
                .addOrderSettings(UserDetailsColumns.UserName, OrderDirection.Ascendant)
                .setPageSettings(1,2);

            listedUsers = users.ToList();
            Assert.Equal(2, listedUsers.Count);

            Assert.Equal("Editor", listedUsers[0].RoleName);
            Assert.Equal("Narciso", listedUsers[0].UserName);
            Assert.Equal("Reader", listedUsers[1].RoleName);
            Assert.Equal("Narciso", listedUsers[1].UserName);
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

                adminRole = new Role { Name = "Admin", Priority = 100 };
                context.Roles.Add(adminRole);
                editorRole = new Role { Name = "Editor", Priority = 50 };
                context.Roles.Add(editorRole);
                readerRole = new Role { Name = "Reader", Priority = 0 };
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
