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

            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();

            ViewBag.Message = "Your application description page." + post.Author + " : " + post.Title;

            return View();
        }
    }
}
