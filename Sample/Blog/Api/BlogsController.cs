using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Core.Data;
using System.Linq;

namespace BlogiFire.Api
{
    [Authorize]
    [Route("blog/api/[controller]")]
    public class BlogsController : Controller
    {
        IBlogRepository _db;
        public BlogsController(IBlogRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Json(await _db.All());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await _db.GetById(id));
        }

        [HttpGet("settings")]
        public async Task<ActionResult> GetSettings()
        {
            var blogs = await _db.Find(b => b.AuthorId == User.Identity.Name);
            var blog = blogs.FirstOrDefault();
            return Json(blog);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Post([FromBody]Blog item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            if (item.Id > 0)
            {
                await _db.Update(item);
            }
            else
            {
                await _db.Add(item);
                Context.Response.StatusCode = 201;
            }
            return new ObjectResult(item);
        }
        
        [HttpDelete("remove/{id:int}")]
        public async Task<string> Delete(int id)
        {
            await _db.Delete(id);
            return "Deleted";
        }
    }
}