using System;

namespace BlogiFire.Core.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ParentId { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Content { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public DateTime Published { get; set; }
        public bool IsApproved { get; set; }
        public bool IsSelected { get; set; }
    }
}