using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BlogiFire.Core.Data
{
    public class CommentRepository : ICommentRepository
    {
        BlogiFireContext _db;
        public CommentRepository()
        {
            _db = new BlogiFireContext();
        }
        public async Task<List<Comment>> All()
        {
            var comments = _db.Comments.AsNoTracking().AsQueryable();
            return await comments.OrderByDescending(c => c.Published).ToListAsync();
        }
        public async Task<List<Comment>> Find(Expression<Func<Comment, bool>> predicate, int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;
            var comments = _db.Comments.AsNoTracking().AsQueryable();

            comments = comments.Where(predicate).OrderByDescending(c => c.Published);

            if (skip == 0)
            {
                return await comments.Take(pageSize).ToListAsync();
            }
            else
            {
                return await comments.Skip(skip).Take(pageSize).ToListAsync();
            }
        }
        public async Task<List<Comment>> GetPostComments(string slug)
        {
            using (var context = new BlogiFireContext())
            {
                var postId = context.Posts.Where(p => p.Slug == slug).FirstOrDefault().Id;
                return await context.Comments.Where(c => c.PostId == postId).ToListAsync();
            }
        }
        public async Task<Comment> GetById(int id)
        {
            return await _db.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task Add(Comment item)
        {
            using (var context = new BlogiFireContext())
            {
                await context.Comments.AddAsync(item);

                // update comment count
                if(item.PostId > 0)
                {
                    var post = context.Posts.Where(p => p.Id == item.PostId).FirstOrDefault();
                    post.Comments++;
                    await context.Posts.UpdateAsync(post);
                }           
                await context.SaveChangesAsync();
            }
        }
        public async Task Update(Comment item)
        {
            using (var context = new BlogiFireContext())
            {
                var itemToUpdate = await context.Comments.FirstOrDefaultAsync(i => i.Id == item.Id);

                itemToUpdate.Author = item.Author;
                itemToUpdate.Content = item.Content;
                itemToUpdate.Ip = item.Ip;
                itemToUpdate.Website = item.Website;
                itemToUpdate.UserAgent = item.UserAgent;
                itemToUpdate.Published = item.Published;
                itemToUpdate.IsApproved = item.IsApproved;       

                await context.Comments.UpdateAsync(itemToUpdate);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            using (var context = new BlogiFireContext())
            {
                var item = await context.Comments.FirstOrDefaultAsync(i => i.Id == id);
                context.Comments.Remove(item);

                // update comment count
                if (item.PostId > 0)
                {
                    var post = context.Posts.Where(p => p.Id == item.PostId).FirstOrDefault();
                    post.Comments--;
                    await context.Posts.UpdateAsync(post);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}