using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Core.Data
{
    public interface ICommentRepository
    {
        Task<List<Comment>> All();
        Task<List<Comment>> Find(Expression<Func<Comment, bool>> predicate, int page = 1, int pageSize = 10);
        Task<Comment> GetById(int id);
        Task Add(Comment item);
        Task Update(Comment item);
        Task Delete(int id);
    }
}