using System.Linq;
using Xunit;
using BlogiFire.Core.Data;
using System.Threading.Tasks;

namespace BlogiFire.Tests.Repository
{
    public class BlogCrudTests
    {
        IBlogRepository _db;
        public BlogCrudTests()
        {
            // use in memory db for tesing
            AppSettings.UseInMemoryDb = true;
            AppSettings.InitializeData = false;
            _db = new BlogRepository();
        }

        [Fact]
        public async Task CanDoCrudOnBlogs()
        {
            var allBlogs = await _db.All();
            var cnt = allBlogs.Count;

            // add
            var blog = new Blog { Title = "blog1", Description = "blog one" };
            await _db.Add(blog);

            allBlogs = await _db.All();
            Assert.Equal(allBlogs.Count, cnt + 1);

            var blogs = await _db.Find(b => b.Title == "blog1");
            var added = blogs.FirstOrDefault();

            Assert.NotNull(added);
            Assert.True(added.Id > 0);

            // update
            added.Description = "blog one updated";
            await _db.Update(added);

            blogs = await _db.Find(b => b.Title == "blog1");
            added = blogs.FirstOrDefault();

            Assert.Equal(added.Description, "blog one updated");

            // delete
            await _db.Delete(added.Id);

            allBlogs = await _db.All();
            Assert.Equal(allBlogs.Count, cnt);
        }
    }
}