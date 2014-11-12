using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

namespace BlogiFire.Web.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostsController : Controller
    {
        Models.BlogiFireContext _context = new Models.BlogiFireContext();

        public IActionResult Index()
        {
            var post = new Core.Data.Entities.Post();

            post.Author = "admin";
            post.Title = "test";
            post.Content = "this is a post";
            post.Published = System.DateTime.UtcNow;

            _context.Database.EnsureCreated();

            _context.Posts.Add(post);
            _context.SaveChanges();

            //_context.Database.EnsureDeleted();
            

            ViewBag.Message = "Your application description page." + post.Author + " : " + post.Title;

            return View();
        }
    }
}
