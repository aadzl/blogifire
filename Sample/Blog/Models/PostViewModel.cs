using BlogiFire.Core.Data;
using System.Collections.Generic;

namespace BlogiFire.Web
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}