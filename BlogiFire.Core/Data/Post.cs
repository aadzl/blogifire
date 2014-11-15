using System;
using System.ComponentModel.DataAnnotations;

namespace BlogiFire.Core.Data
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public bool Published { get; set; }
    }
}