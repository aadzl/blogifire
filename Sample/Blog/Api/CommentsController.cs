using BlogiFire.Core.Data;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogiFire.Api
{
    [Authorize]
    [Route("blog/api")]
    public class CommentsController : Controller
    {
        ICommentRepository commentsDb;
        IPostRepository postsDb;

        public CommentsController(ICommentRepository commentsDb, IPostRepository postsDb)
        {
            this.commentsDb = commentsDb;
            this.postsDb = postsDb;
        }

        [Route("comments")]
        public async Task<ActionResult> Get()
        {
            var comments = new List<Comment>();
            var posts = await postsDb.GetBlogPosts(User.Identity.Name);

            foreach (var post in posts)
            {
                var postComments = await commentsDb.Find(c => c.PostId == post.Id);
                comments.AddRange(postComments);
            }
                
            return Json(comments);
        }

        [Route("comments/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await commentsDb.GetById(id));
        }

        [Route("comments/post/{id:int}")]
        public async Task<ActionResult> GetByPost(int id)
        {
            var comments = await commentsDb.Find(c => c.PostId == id);
            return Json(comments);
        }

        [Route("comments/add")]
        public async Task<ActionResult> Post([FromBody]Comment item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            if (item.Id > 0)
            {
                await commentsDb.Update(item);
            }
            else
            {
                await commentsDb.Add(item);
                Context.Response.StatusCode = 201;
            }
            return new ObjectResult(item);
        }
        
        [Route("comments/remove/{id:int}")]
        public async Task<string> Delete(int id)
        {
            await commentsDb.Delete(id);
            return "Deleted";
        }
    }
}