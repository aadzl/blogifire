using System;
using System.Collections.Generic;

namespace BlogiFire.Core.Data
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAll { get; }
        Post GetById(int id);
        void Add(Post item);
        bool Delete(int id);
    }
}