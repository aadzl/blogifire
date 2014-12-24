using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    db.SaveChanges();

                    // add comments to every third post
                    if (i % 3 == 0)
                    {
                        var comments = GetComments();
                        foreach (var com in comments)
                        {
                            var c = new Comment
                            {
                                Author = "visitor" + i.ToString(),
                                Email = "visitor" + i.ToString() + "@us.com",
                                Content = com,
                                PostId = post.Id,
                                IsApproved = true,
                                Published = DateTime.UtcNow.AddHours((i - 15) * 5 * blog.Id)
                            };
                            db.Comments.Add(c);
                            post.Comments++;
                        }
                        db.SaveChanges();
                    }
                }
                
            }
        }

        string GetContent()
        {
            var sb = new StringBuilder();
            int iRnd = _rnd.Next(3, 10); 
            
            // for every post, randomly get 3 to 10 paragraphs
            for (int i = 0; i < iRnd; i++)
            {
                int iRnd2 = _rnd.Next(0, SeedData.Paragraphs.Length);
                var par = SeedData.Paragraphs[iRnd2];
                if (par.StartsWith("<"))
                {
                    sb.Append(par);
                }
                else
                {
                    sb.Append("<p>" + par + "</p>");
                }
            }
            return sb.ToString();
        }

        List<string> GetComments()
        {
            int commCnt = _rnd.Next(1, 5);
            var comments = new List<string>();

            for (int i = 0; i < commCnt; i++)
            {
                int commRnd = _rnd.Next(0, SeedData.Comments.Length);
                comments.Add(SeedData.Comments[commRnd]);
            }
            return comments;
        }
    }
}