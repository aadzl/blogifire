using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using BlogiFire.Core.Data;
using BlogiFire.Web.Models;

namespace BlogiFire.Web.Api.Controllers
{
    [Route("blog/api/[controller]")]
    public class PostsController : Controller
    {
        public PostsController()
        {
        }

        // GET: blog/api/posts
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            using (var db = new BlogModels())
            {
                return db.Posts.OrderBy(b => b.Title);
            }
        }

        // GET blog/api/posts/2
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            using (var db = new BlogModels())
            {
                return db.Posts.Where(p => p.Id == id).FirstOrDefault();
            }
        }

        // POST blog/api/posts
        [HttpPost]
        public void Post([FromBody]Post post)
        {
            using (var db = new BlogModels())
            {
                db.Add(post);
                db.SaveChanges();
            }
        }

        // PUT blog/api/posts/2
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Post post)
        {
            var x = post;
        }

        // DELETE blol/api/posts/2
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new BlogModels())
            {
                var post = db.Posts.Where(p => p.Id == id).FirstOrDefault();
                db.Delete(post);
                db.SaveChanges();
            }
        }
    }
}
