using System;

namespace BlogiFire.Core.Data
{
    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int Comments { get; set; }
        public bool CommentsEnabled { get; set; }
        public DateTime Saved { get; set; }
        public DateTime Published { get; set; }
        public string AuthorName { get; set; }
        public bool Visible
        {
            get
            {
                if (Published == DateTime.MinValue)
                    return false;

                return Published <= DateTime.UtcNow ? true : false;
            }
        }
        public bool IsSelected { get; set; }
    }
}