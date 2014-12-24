using BlogiFire.Core.Data;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogiFire.Api
{
    [Authorize]
    [Route("blog/api/comments")]
    public class CommentsController : Controller
    {
        #region constructor

        ICommentRepository _commentsDb;
        IPostRepository _postsDb;

        public CommentsController(ICommentRepository commentsDb, IPostRepository postsDb)
        {
            _commentsDb = commentsDb;
            _postsDb = postsDb;
        }

        #endregion

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var comments = new List<Comment>();
            var posts = await _postsDb.GetBlogPosts(User.Identity.Name);

            foreach (var post in posts)
            {
                var postComments = await _commentsDb.Find(c => c.PostId == post.Id);
                comments.AddRange(postComments);
            }              
            return Json(comments.OrderByDescending(c => c.Published));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await _commentsDb.GetById(id));
        }

        [HttpGet("post/{id:int}")]
        public async Task<ActionResult> GetByPost(int id)
        {
            var comments = await _commentsDb.Find(c => c.PostId == id);
            return Json(comments);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Post([FromBody]Comment item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            if (item.Id > 0)
            {
                await _commentsDb.Update(item);
            }
            else
            {
                await _commentsDb.Add(item);
                Context.Response.StatusCode = 201;
            }
            return new ObjectResult(item);
        }

        [HttpPut("{operation}")]
        public async Task<string> Process([FromBody]List<Comment> items, string operation)
        {
            foreach (var item in items)
            {
                if (operation == "delete")
                {
                    await _commentsDb.Delete(item.Id);
                }
                if (operation == "approve")
                {
                    item.IsApproved = true;
                    item.Published = DateTime.UtcNow;
                    await _commentsDb.Update(item);
                }
                if (operation == "archive")
                {
                    item.IsApproved = false;
                    item.Published = DateTime.MinValue;
                    await _commentsDb.Update(item);
                }
            }
            return "Processed";
        }
    }
}