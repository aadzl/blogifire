using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Core.Data;
using System.Collections.Generic;

namespace BlogiFire.Api
{
    [Authorize]
    [Route("blog/api/[controller]")]
    public class PostsController : Controller
    {
        #region Constructor and private members

        IPostRepository postsDb;
        IBlogRepository blogsDb;
        public PostsController(IPostRepository postsDb, IBlogRepository blogsDb)
        {
            this.postsDb = postsDb;
            this.blogsDb = blogsDb;
        }

        #endregion

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var blogs = await blogsDb.Find(b => b.AuthorId == User.Identity.Name);
            if (blogs == null || blogs.Count == 0)
                return null;

            var blogId = blogs.FirstOrDefault().Id;
            var posts = await postsDb.All();
            return Json(posts.Where(p => p.BlogId == blogId).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await postsDb.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Post item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            try
            {
                //item.Saved = DateTime.UtcNow;

                if (item.Id > 0)
                {
                    await postsDb.Update(item);
                }
                else
                {
                    var blogs = await blogsDb.Find(b => b.AuthorId.ToLower() == User.Identity.Name);

                    if (blogs.Count > 0)
                    {
                        var blog = blogs.FirstOrDefault();
                        item.BlogId = blog.Id;
                    }

                    await postsDb.Add(item);
                    Context.Response.StatusCode = 201;
                }
                return new ObjectResult(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPut("{operation}")]
        public async Task<string> Process([FromBody]List<Post> items, string operation)
        {
            foreach (var item in items)
            {
                if(operation == "delete")
                {
                    await postsDb.Delete(item.Id);
                }
                if (operation == "publish")
                {
                    item.Published = DateTime.UtcNow;
                    await postsDb.Update(item);
                }
                if (operation == "archive")
                {
                    item.Published = DateTime.MinValue;
                    await postsDb.Update(item);
                }
            }
            return "Processed";
        }
    }
}