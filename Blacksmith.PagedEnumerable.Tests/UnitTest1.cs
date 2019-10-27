using Blacksmith.PagedEnumerable.Exceptions;
using Blacksmith.PagedEnumerable.Extensions;
using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.PagedEnumerable.Services;
using Blacksmith.PagedEnumerable.Tests.Contexts;
using Blacksmith.PagedEnumerable.Tests.Models;
using Blacksmith.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blacksmith.PagedEnumerable.Tests
{
    public class UnitTest1
    {
        private readonly TestContext context;
        private readonly IPagedEnumerableBuilder pagedEnumerableBuilder;

        public UnitTest1()
        {
            this.context = prv_getContext();
            IPagedEnumerableStrings strings = new EnPagedEnumerableStrings();
            this.pagedEnumerableBuilder = new DefaultPagedEnumerableBuilder(strings);
        }

        [Fact]
        public void usual_pagination()
        {
            IPagedEnumerable<User> users;
            IList<User> listedUsers;

            users = this.context
                .Users
                .paginate()
                .setPageSize(2)
                .setCurrentPage(1);

            Assert.Equal(5, users.TotalCount);
            Assert.Equal(2, users.Count());

            listedUsers = users.ToList();
            Assert.Equal(2, listedUsers.Count);
            Assert.Equal("Rosa", listedUsers[0].Name);
            Assert.Equal("Pepe", listedUsers[1].Name);
        }

        [Fact]
        public void default_pagination()
        {
            IPagedEnumerable<User> users;
            IList<User> listedUsers;

            users = this.context
                .Users
                .paginate();

            Assert.Equal(5, users.TotalCount);
            Assert.Equal(5, users.Count());

            listedUsers = users.ToList();
            Assert.Equal(5, listedUsers.Count);
            Assert.Equal("Narciso", listedUsers[0].Name);
            Assert.Equal("Tronco", listedUsers[4].Name);
        }

        [Fact]
        public void mapped_pagination()
        {
            IPagedEnumerable<UserDetails, UserDetailsColumns> users;
            IList<User> listedUsers;

            users = prv_getUserDetails(this.context)
                ;
            

            Assert.Equal(5, users.TotalCount);
            Assert.Equal(5, users.Count());

            listedUsers = users.ToList();
            Assert.Equal(5, listedUsers.Count);
            Assert.Equal("Narciso", listedUsers[0].Name);
            Assert.Equal("Tronco", listedUsers[4].Name);
        }

        private IPagedEnumerable<UserDetails, UserDetailsColumns> prv_getUserDetails(TestContext context)
        {
            return this.pagedEnumerableBuilder
                .buildFor(this.context.UserRoles, prv_orderMap, prv_map);
        }

        private static UserDetails prv_map(UserRole userRole)
        {
            return new UserDetails
            {
                UserName = userRole.User.Name,
                RoleName = userRole.Role.Name,
                Active = userRole.Active,
            };
        }

        private static void prv_orderMap(UserDetailsColumns column, OrderDirection direction, )


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
                context.Users.Add(new User { Name = "Narciso" });
                context.Users.Add(new User { Name = "Florencio" });
                context.Users.Add(new User { Name = "Rosa" });
                context.Users.Add(new User { Name = "Pepe" });
                context.Users.Add(new User { Name = "Tonco" });
                context.SaveChanges();

                context.Roles.Add(new Role { Name = "Reader" });
                context.Roles.Add(new Role { Name = "Editor" });
                context.Roles.Add(new Role { Name = "Admin" });
                context.SaveChanges();

                
            }
            throw new NotImplementedException();
        }
    }
}
