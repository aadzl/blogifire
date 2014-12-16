using BlogiFire.Core.Data;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BlogiFire.Controllers
{
    [Route("blog")]
    public class CommentsController : Controller
    {
        #region Constructor and private memeber

        ICommentRepository commentsDb;
        int pageSize = 10;

        public CommentsController(ICommentRepository commentsDb, IBlogRepository blogsDb)
        {
            this.commentsDb = commentsDb;
        }

        #endregion

        // GET: blog
        [Route("comments")]
        public async Task<IActionResult> Index()
        {
            int page = 1;
            ViewBag.Title = "Blog comments ";
            var comments = await commentsDb.Find(p => p.IsApproved == true, page, pageSize);

            //ViewBag.NewerPage = 0;
            //await GetOlderPage(page);

            return View("~/Blog/Views/Comments/Index.cshtml", comments.ToList());
        }

        // GET: blog/comment/my-comment
        [Route("comments/{id}")]
        public async Task<IActionResult> Single(int id)
        {
            var comments = await commentsDb.Find(c => c.Id == id);
            var comment = comments.FirstOrDefault();
            ViewBag.Title = comment.Author;

            return View("~/Blog/Views/Comments/Comment.cshtml", comment);
        }

        [Route("comments/add")]
        public async Task<ActionResult> Post([FromBody]Comment item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            item.Published = DateTime.UtcNow;
            await commentsDb.Add(item);

            Context.Response.StatusCode = 201;
            return new ObjectResult("success");
        }
    }
}