using BlogiFire.Core.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogiFire.Core.Services
{
    public class Syndication
    {
        public async Task<RssViewModel> GetAppRss()
        {
            var feed = new RssViewModel();
            feed.Posts = new List<PostView>();

            using (var context = new BlogiFireContext())
            {
                var posts = await context.Posts.Where(p => p.Visible == true).
                    OrderByDescending(p => p.Published).Take(AppSettings.PageSize).ToListAsync();

                feed.Title = AppSettings.Title;
                feed.Description = AppSettings.Description;
                feed.Link = AppSettings.AbsoluteUrl;

                foreach (var p in posts)
                {
                    feed.Posts.Add(new PostView
                    {
                        Title = p.Title,
                        Author = p.AuthorName,
                        Description = p.Content,
                        Link = AppSettings.AbsoluteUrl + "/post/" + p.Slug,
                        Updated = p.Published
                    });
                }
            }
            return feed;
        }

        public async Task<RssViewModel> GetBlogRss(string author)
        {
            var feed = new RssViewModel();
            feed.Posts = new List<PostView>();

            using (var context = new BlogiFireContext())
            {
                var blog = context.Blogs.Where(b => b.AuthorId == author).FirstOrDefault();

                var posts = await context.Posts.Where(p => p.BlogId == blog.Id && p.Visible == true)
                    .OrderByDescending(p => p.Published).Take(AppSettings.PageSize).ToListAsync();

                feed.Title = blog.Title;
                feed.Description = blog.Description;
                feed.Link = AppSettings.AbsoluteUrl;

                foreach (var p in posts)
                {
                    feed.Posts.Add(new PostView
                    {
                        Title = p.Title,
                        Author = p.AuthorName,
                        Description = p.Content,
                        Link = AppSettings.AbsoluteUrl + "/post/" + p.Slug,
                        Updated = p.Published
                    });
                }
            }
            return feed;
        }
    }
}