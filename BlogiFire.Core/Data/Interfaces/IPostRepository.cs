using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public interface IPostRepository
    {
        Task<List<Post>> All();
        Task<List<Post>> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10);
        Task<Post> GetById(int id);
        Task<List<Post>> GetBlogPosts(string author);
        Task Add(Post item);
        Task Update(Post item);
        Task Delete(int id);
    }
}