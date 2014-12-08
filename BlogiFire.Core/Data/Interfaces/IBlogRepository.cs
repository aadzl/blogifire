using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public interface IBlogRepository
    {
        Task<List<Blog>> All();
        Task<List<Blog>> Find(Expression<Func<Blog, bool>> predicate);
        Task<Blog> GetById(int id);
        Task Add(Blog item);
        Task Update(Blog item);
        Task Delete(int id);
    }
}