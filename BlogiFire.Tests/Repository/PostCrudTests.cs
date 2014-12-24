using System.Linq;
using Xunit;
using BlogiFire.Core.Data;
using System.Threading.Tasks;

namespace BlogiFire.Tests.Repository
{
    public class PostCrudTests
    {
        IPostRepository _db;
        public PostCrudTests()
        {
            // use in memory db for tesing
            AppSettings.UseInMemoryDb = true;
            AppSettings.InitializeData = false;
            _db = new PostRepository();
        }

        [Fact]
        public async Task CanDoCrudOnPosts()
        {
            var allPosts = await _db.All();
            var cnt = allPosts.Count;

            // add
            var post = new Post { Title = "test1", Slug = "test-1", Content = "test one" };
            await _db.Add(post);

            allPosts = await _db.All();
            Assert.Equal(allPosts.Count, cnt + 1);

            var posts = await _db.Find(p => p.Slug == "test-1");
            var added = posts.FirstOrDefault();

            Assert.NotNull(added);
            Assert.True(added.Id > 0);

            System.Diagnostics.Debug.WriteLine("before update");
            // update
            added.Content = "test one updated";
            await _db.Update(added);

            posts = await _db.Find(p => p.Slug == "test-1");
            added = posts.FirstOrDefault();

            Assert.Equal(added.Content, "test one updated");

            // delete
            await _db.Delete(added.Id);

            allPosts = await _db.All();
            Assert.Equal(allPosts.Count, cnt);
        }
    }
}