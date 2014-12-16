using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public class DataInitializer
    {
        public void SeedBlogs()
        {
            var bob = new Blog { Title = "Bog's blog", Description = "This is bob's blog", AuthorId = "Bob", AuthorEmail = "bob@us", AuthorName = "Mr. Bob" };
            var sam = new Blog { Title = "Sam's blog", Description = "This is sam's blog", AuthorId = "Sam", AuthorEmail = "sam@us", AuthorName = "Mr. Sam" };

            using (var db = new BlogiFireContext())
            {
                if(db.Blogs.Where(b => b.AuthorId == "Bob").FirstOrDefault() == null)
                {
                    var b1 = db.Blogs.Add(bob);
                    db.SaveChanges();
                    SeedPosts(b1);
                }
                if (db.Blogs.Where(b => b.AuthorId == "Sam").FirstOrDefault() == null)
                {
                    var b2 = db.Blogs.Add(sam);
                    db.SaveChanges();
                    SeedPosts(b2);
                }
            }
        }

        Random _rnd = new Random();

        public void SeedPosts(Blog blog)
        {
            using (var db = new BlogiFireContext())
            {
                for (int i = 1; i <= 25; i++)
                {
                    var post = new Post();
                    var blogContent = GetContent();

                    post.BlogId = blog.Id;
                    post.AuthorName = blog.AuthorName;
                    post.Title = string.Format("Post title{0} from {1}", i, blog.AuthorId);
                    post.Slug = string.Format("post-title{0}-{1}", i, blog.AuthorId);
                    post.Content = string.Format("This is content of the post {0} written by {1}. {2}", i, blog.AuthorName, blogContent);
                    post.Published = DateTime.UtcNow.AddHours((i - 26) * 5 * blog.Id);
                    post.Saved = post.Published;

                    db.Posts.Add(post);
                }
                db.SaveChanges();
            }
        }

        public void SeedPosts2()
        {
            for (int i = 1; i <= 15; i++)
            {
                var blogContent = string.Format("Paragraphs for post {0} :: {1}", i, GetContent());
                System.Diagnostics.Debug.WriteLine(blogContent);
            }
        }

        string GetContent()
        {
            var sb = new StringBuilder();
            int iRnd = _rnd.Next(3, 20); 
            
            // for every post, randomly get 3 to 20 paragraphs
            for (int i = 0; i < iRnd; i++)
            {
                int iRnd2 = _rnd.Next(0, SeedData.Paragraphs.Length);
                sb.Append("<br/>" + SeedData.Paragraphs[iRnd2]);
            }
            return sb.ToString();
        }

        string GetParagraph()
        {
            Random rnd2 = new Random();
            int iRnd2 = rnd2.Next(0, SeedData.Paragraphs.Length);

            return iRnd2.ToString(); // SeedData.Paragraphs[iRnd2];
        }
    }
}