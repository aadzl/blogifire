using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public class BlogRepository : IBlogRepository
    {
        BlogiFireContext _db;
        public BlogRepository()
        {
            _db = new BlogiFireContext();
        }
        public async Task<List<Blog>> All()
        {
            return await _db.Blogs.OrderBy(b => b.Title).ToListAsync();
        }
        public async Task<List<Blog>> Find(Expression<Func<Blog, bool>> predicate)
        {
            return await _db.Blogs.Where(predicate).ToListAsync();
        }
        public async Task<Blog> GetById(int id)
        {
            return await _db.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task Add(Blog item)
        {
            _db.Blogs.Add(item);
            await _db.SaveChangesAsync();

            if (AppSettings.InitializeData)
            {
                var initializer = new DataInitializer();
                initializer.SeedPosts(item);
            }
        }
        public async Task Update(Blog item)
        {
            try
            {
                var dbItem = _db.Blogs.SingleOrDefault(i => i.Id == item.Id);

                dbItem.Title = item.Title;

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task Delete(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(i => i.Id == id);
            _db.Blogs.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}