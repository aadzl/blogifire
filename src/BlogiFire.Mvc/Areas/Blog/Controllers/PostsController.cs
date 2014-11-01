using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

namespace BlogiFire.Mvc.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
