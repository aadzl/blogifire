using System;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public class DataInitializer
    {
        //async Task CreateBlogs()
        //{
        //    for (int i = 1; i <= 3; i++)
        //    {
        //        var blog = new Blog();
        //        blog.AuthorId = string.Format("user{0}", i);
        //        blog.AuthorName = string.Format("User {0}", i);
        //        blog.AuthorEmail = blog.AuthorId + "@us.com";
        //        blog.Title = "Blog for " + blog.AuthorId;
        //        blog.Description = "This is a blog by " + blog.AuthorName;

        //        db.Blogs.Add(blog);
        //        await db.SaveChangesAsync();

        //        await CreatePosts(blog);
        //    }
        //}

        public async Task SeedPosts(Blog blog)
        {
            using (var db = new BlogiFireContext())
            {
                for (int i = 1; i <= 15; i++)
                {
                    var post = new Post();

                    post.BlogId = blog.Id;
                    post.AuthorName = blog.AuthorName;
                    post.Title = string.Format("Post title{0} from {1}", i, blog.AuthorId);
                    post.Slug = string.Format("post-title{0}-{1}", i, blog.AuthorId);
                    post.Content = string.Format("This is content of the post {0} written by {1}.", i, blog.AuthorName);
                    post.Published = DateTime.UtcNow.AddHours((i - 26) * 5 * blog.Id);
                    post.Saved = post.Published;

                    await db.Posts.AddAsync(post);
                }
                await db.SaveChangesAsync();
            }
        }
    }
}