using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogiFire.Core.Data
{
    public class PostRepository : IPostRepository
    {
        readonly List<Post> _items = new List<Post>();
        public IEnumerable<Post> GetAll
        {
            get
            {
                return _items;
            }
        }

        public Post GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Post item)
        {
            item.Id = 1 + _items.Max(x => (int?)x.Id) ?? 0;
            _items.Add(item);
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item == null)
            {
                return false;
            }
            _items.Remove(item);
            return true;
        }
    }
}