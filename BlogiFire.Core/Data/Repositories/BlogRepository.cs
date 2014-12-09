using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public class BlogRepository : IBlogRepository
    {
        BlogiFireContext db;
        public BlogRepository()
        {
            this.db = new BlogiFireContext();
        }
        public async Task<List<Blog>> All()
        {
            return await db.Blogs.OrderBy(b => b.Title).ToListAsync();
        }
        public async Task<List<Blog>> Find(Expression<Func<Blog, bool>> predicate)
        {
            return await db.Blogs.Where(predicate).ToListAsync();
        }
        public async Task<Blog> GetById(int id)
        {
            return await db.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task Add(Blog item)
        {
            db.Blogs.Add(item);
            await db.SaveChangesAsync();

            if (AppSettings.InitializeData)
            {
                var initializer = new DataInitializer();
                await initializer.SeedPosts(item);
            }
        }
        public async Task Update(Blog item)
        {
            try
            {
                var dbItem = db.Blogs.SingleOrDefault(i => i.Id == item.Id);

                dbItem.Title = item.Title;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task Delete(int id)
        {
            var item = await db.Blogs.FirstOrDefaultAsync(i => i.Id == id);
            db.Blogs.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}