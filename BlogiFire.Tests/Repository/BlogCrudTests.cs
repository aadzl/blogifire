using System.Linq;
using Xunit;
using BlogiFire.Core.Data;
using System.Threading.Tasks;

namespace BlogiFire.Tests.Repository
{
    public class BlogCrudTests
    {
        IBlogRepository db;
        public BlogCrudTests()
        {
            // use in memory db for tesing
            AppSettings.UseInMemoryDb = true;
            AppSettings.InitializeData = false;
            db = new BlogRepository();
        }

        [Fact]
        public async Task CanDoCrudOnBlogs()
        {
            var allBlogs = await db.All();
            var cnt = allBlogs.Count;

            // add
            var blog = new Blog { Title = "blog1", Description = "blog one" };
            await db.Add(blog);

            allBlogs = await db.All();
            Assert.Equal(allBlogs.Count, cnt + 1);

            var blogs = await db.Find(b => b.Title == "blog1");
            var added = blogs.FirstOrDefault();

            Assert.NotNull(added);
            Assert.True(added.Id > 0);

            // update
            added.Description = "blog one updated";
            await db.Update(added);

            blogs = await db.Find(b => b.Title == "blog1");
            added = blogs.FirstOrDefault();

            Assert.Equal(added.Description, "blog one updated");

            // delete
            await db.Delete(added.Id);

            allBlogs = await db.All();
            Assert.Equal(allBlogs.Count, cnt);
        }
    }
}