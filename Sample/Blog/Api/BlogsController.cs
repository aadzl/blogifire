using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Core.Data;
using System.Linq;

namespace BlogiFire.Api
{
    [Authorize]
    [Route("blog/api")]
    public class BlogsController : Controller
    {
        IBlogRepository db;
        public BlogsController(IBlogRepository db)
        {
            this.db = db;
        }

        [Route("blogs")]
        public async Task<ActionResult> Get()
        {
            return Json(await db.All());
        }

        [Route("blogs/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await db.GetById(id));
        }

        [Route("settings")]
        public async Task<ActionResult> GetSettings()
        {
            var blogs = await db.Find(b => b.AuthorId == User.Identity.Name);
            var blog = blogs.FirstOrDefault();
            return Json(blog);
        }

        [Route("blogs/add")]
        public async Task<ActionResult> Post([FromBody]Blog item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            if (item.Id > 0)
            {
                await db.Update(item);
            }
            else
            {
                await db.Add(item);
                Context.Response.StatusCode = 201;
            }
            return new ObjectResult(item);
        }
        
        [Route("blogs/remove/{id:int}")]
        public async Task<string> Delete(int id)
        {
            await db.Delete(id);
            return "Deleted";
        }
    }
}