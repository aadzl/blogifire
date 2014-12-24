using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BlogiFire.Core.Data
{
    public class PostRepository : IPostRepository
    {
        BlogiFireContext _db;
        public PostRepository()
        {
            _db = new BlogiFireContext();
        }
        public async Task<List<Post>> All()
        {
            var posts = _db.Posts.AsNoTracking().AsQueryable();
            return await posts.OrderByDescending(p => p.Published).ToListAsync();
        }
        public async Task<List<Post>> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;
            var posts = _db.Posts.AsNoTracking().AsQueryable();

            posts = posts.Where(predicate).OrderByDescending(p => p.Published);

            if (skip == 0)
            {
                return await posts.Take(pageSize).ToListAsync();
            }
            else
            {
                return await posts.Skip(skip).Take(pageSize).ToListAsync();
            }
        }
        public async Task<Post> GetById(int id)
        {
            return await _db.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Post>> GetBlogPosts(string author)
        {
            using (var context = new BlogiFireContext())
            {
                var blogId = context.Blogs.Where(b => b.AuthorId == author).FirstOrDefault().Id;
                return await context.Posts.Where(p => p.BlogId == blogId).ToListAsync();
            }
        }
        public async Task Add(Post item)
        {
            using (var context = new BlogiFireContext())
            {
                await context.Posts.AddAsync(item);
                await context.SaveChangesAsync();
            }
        }
        public async Task Update(Post item)
        {
            using (var context = new BlogiFireContext())
            {
                var itemToUpdate = await context.Posts.FirstOrDefaultAsync(i => i.Id == item.Id);

                itemToUpdate.Saved = DateTime.UtcNow;
                itemToUpdate.Title = item.Title;
                itemToUpdate.Content = item.Content;
                itemToUpdate.Tags = item.Tags;
                itemToUpdate.Published = item.Published;        

                await context.Posts.UpdateAsync(itemToUpdate);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            using (var context = new BlogiFireContext())
            {
                var item = await context.Posts.FirstOrDefaultAsync(i => i.Id == id);
                context.Posts.Remove(item);
                await context.SaveChangesAsync();
            }
        }
    }
}