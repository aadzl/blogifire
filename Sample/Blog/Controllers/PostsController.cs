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

        IPostRepository postsDb;
        IBlogRepository blogsDb;
        ICommentRepository commentsDb;
        int pageSize;
        public PostsController(IPostRepository postsDb, IBlogRepository blogsDb, ICommentRepository commentsDb)
        {
            this.postsDb = postsDb;
            this.blogsDb = blogsDb;
            this.commentsDb = commentsDb;
            pageSize = 10;
        }

        #endregion

        // GET: blog
        public async Task<IActionResult> Index()
        {
            int page = 1;
            ViewBag.Title = "Blog posts ";
            var posts = await postsDb.Find(p => p.Visible == true, page, pageSize);

            ViewBag.NewerPage = 0;
            await GetOlderPage(page);

            return View("~/Blog/Views/Posts/Index.cshtml", posts.ToList());
        }

        // GET: blog/page/2
        [Route("page/{page:int}")]
        public async Task<IActionResult> Page(int page)
        {
            ViewBag.Title = "Blog posts ";
            var posts = await postsDb.Find(p => p.Visible == true, page, pageSize);

            ViewBag.NewerPage = page - 1;
            await GetOlderPage(page);

            return View("~/Blog/Views/Posts/Index.cshtml", posts.ToList());
        }

        // GET: blog/post/my-post
        [Route("post/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            var vm = new PostViewModel();
            var posts = await postsDb.Find(p => p.Slug == slug);
            vm.Post = posts.FirstOrDefault();
            vm.Comments = await commentsDb.Find(c => c.PostId == vm.Post.Id);
            ViewBag.Title = vm.Post.Title;

            return View("~/Blog/Views/Posts/Post.cshtml", vm);
        }

        // get page number by quering if older records exist
        private async Task GetOlderPage(int page)
        {
            ViewBag.OlderPage = page + 1;
            var olderPosts = await postsDb.Find(p => p.Visible == true, page + 1, pageSize);
            if (olderPosts == null || olderPosts.Count() < 1)
            {
                ViewBag.OlderPage = 0;
            }
        }
    }
}