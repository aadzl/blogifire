using BlogiFire.Core.Data;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogiFire.Controllers
{
    [Route("blog/comments")]
    public class CommentsController : Controller
    {
        #region Constructor and private memeber

        ICommentRepository _commentsDb;
        int pageSize = 10;

        public CommentsController(ICommentRepository commentsDb)
        {
            _commentsDb = commentsDb;
        }

        #endregion

        // GET: blog/comments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int page = 1;
            ViewBag.Title = "Blog comments ";

            var comments = await _commentsDb.Find(p => p.IsApproved == true, page, pageSize);
            return View("~/Blog/Views/Comments/Index.cshtml", comments.ToList());
        }

        // GET: blog/comments/2
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Single(int id)
        {
            var comments = await _commentsDb.Find(c => c.Id == id);
            var comment = comments.FirstOrDefault();
            ViewBag.Title = comment.Author;

            return View("~/Blog/Views/Comments/Comment.cshtml", comment);
        }

        // GET: blog/comments/post-slug
        [HttpGet("{slug}")]
        public async Task<IActionResult> PostComments(string slug)
        {
            var comments = await _commentsDb.GetPostComments(slug);
            return Json(comments);
        }

        // POST: blog/comments
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Comment item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }
            var ip = Context.GetFeature<Microsoft.AspNet.HttpFeature.IHttpConnectionFeature>();
            if (ip != null)
            {
                item.Ip = ip.RemoteIpAddress.ToString();
            }
            item.UserAgent = Request.Headers["User-Agent"];
            // var refer = Request.Headers["Referer"];

            item.Published = DateTime.UtcNow;
            await _commentsDb.Add(item);

            Context.Response.StatusCode = 201;
            return new ObjectResult("success");
        }
    }
}