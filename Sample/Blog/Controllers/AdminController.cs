using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

namespace Sample.Blog.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Admin";
            return View("~/Blog/Views/Admin/Index.cshtml");
        }
    }
}