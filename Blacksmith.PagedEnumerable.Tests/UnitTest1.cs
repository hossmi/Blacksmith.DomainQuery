using Blacksmith.PagedEnumerable.Extensions;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.PagedEnumerable.Tests.Contexts;
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

        public UnitTest1()
        {
            this.context = prv_getContext();
        }

        [Fact]
        public void test_usual_pagination()
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
        public void test_default_pagination()
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
