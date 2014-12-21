using BlogiFire.Core.Data;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlogiFire.Web
{
    [Route("blog")]
    public class PostsController : Controller
    {
        #region Constructor and private memeber

        IPostRepository _postDb;
        IBlogRepository _blogDb;
        int pageSize;
        public PostsController(IPostRepository postsDb, IBlogRepository blogDb)
        {
            _postDb = postsDb;
            _blogDb = blogDb;
            pageSize = 10;
        }

        #endregion

        // GET: blog
        public async Task<IActionResult> Index()
        {
            int page = 1;
            ViewBag.Title = "Blog posts ";
            var posts = await _postDb.Find(p => p.Visible == true, page, pageSize);

            ViewBag.NewerPage = 0;
            await GetOlderPage(page);

            return View("~/Blog/Views/Posts/Index.cshtml", posts.ToList());
        }

        // GET: blog/page/2
        [Route("page/{page:int}")]
        public async Task<IActionResult> Page(int page)
        {
            ViewBag.Title = "Blog posts ";
            var posts = await _postDb.Find(p => p.Visible == true, page, pageSize);

            ViewBag.NewerPage = page - 1;
            await GetOlderPage(page);

            return View("~/Blog/Views/Posts/Index.cshtml", posts.ToList());
        }

        // GET: blog/post/my-post
        [Route("post/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            var posts = await _postDb.Find(p => p.Slug == slug);
            var vm = posts.FirstOrDefault();
            ViewBag.Title = vm.Title;

            return View("~/Blog/Views/Posts/Post.cshtml", vm);
        }

        // GET: blog/rss
        [Route("rss")]
        public async Task<IActionResult> Rss()
        {
            var syndication = new Core.Services.Syndication();
            var vm = await syndication.GetAppRss();

            return View("~/Blog/Views/Posts/Rss.cshtml", vm);
        }

        // GET: blog/rss/bob
        [Route("rss/{author}")]
        public async Task<IActionResult> Rss(string author)
        {
            var syndication = new Core.Services.Syndication();
            var vm = await syndication.GetBlogRss(author);

            return View("~/Blog/Views/Posts/Rss.cshtml", vm);
        }

        // get page number by quering if older records exist
        private async Task GetOlderPage(int page)
        {
            ViewBag.OlderPage = page + 1;
            var olderPosts = await _postDb.Find(p => p.Visible == true, page + 1, pageSize);
            if (olderPosts == null || olderPosts.Count() < 1)
            {
                ViewBag.OlderPage = 0;
            }
        }
    }
}