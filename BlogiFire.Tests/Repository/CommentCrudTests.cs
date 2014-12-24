using System.Linq;
using Xunit;
using BlogiFire.Core.Data;
using System.Threading.Tasks;

namespace BlogiFire.Tests.Repository
{
    public class CommentCrudTests
    {
        ICommentRepository _db;
        public CommentCrudTests()
        {
            // use in memory db for tesing
            AppSettings.UseInMemoryDb = true;
            AppSettings.InitializeData = false;
            _db = new CommentRepository();
        }

        [Fact]
        public async Task CanDoCrudOnComments()
        {
            var allComments = await _db.All();
            var cnt = allComments.Count;

            // add
            var comment = new Comment { Author = "test1", Email = "test@us.com", Content = "test one" };
            await _db.Add(comment);

            allComments = await _db.All();
            Assert.Equal(allComments.Count, cnt + 1);

            var comments = await _db.Find(c => c.Author == "test1");
            var added = comments.FirstOrDefault();

            Assert.NotNull(added);
            Assert.True(added.Id > 0);

            // update
            added.Content = "test one updated";
            await _db.Update(added);

            comments = await _db.Find(c => c.Author == "test1");
            added = comments.FirstOrDefault();

            Assert.Equal(added.Content, "test one updated");

            // delete
            await _db.Delete(added.Id);

            allComments = await _db.All();
            Assert.Equal(allComments.Count, cnt);
        }
    }
}