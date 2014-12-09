using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BlogiFire.Core.Data
{
    public class PostRepository : IPostRepository
    {
        BlogiFireContext db;
        public PostRepository()
        {
            this.db = new BlogiFireContext();
        }
        public async Task<List<Post>> All()
        {
            var posts = db.Posts.AsNoTracking().AsQueryable();
            return await posts.OrderByDescending(p => p.Published).ToListAsync();
        }
        public async Task<List<Post>> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;
            var posts = db.Posts.AsNoTracking().AsQueryable();

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
            return await db.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task Add(Post item)
        {
            await db.Posts.AddAsync(item);
            await db.SaveChangesAsync();
        }
        public async Task Update(Post item)
        {
            item.Saved = DateTime.UtcNow;
            await db.Posts.UpdateAsync(item);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var item = await db.Posts.FirstOrDefaultAsync(i => i.Id == id);
            db.Posts.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}