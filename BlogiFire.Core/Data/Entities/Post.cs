using System;
using System.ComponentModel.DataAnnotations;

namespace BlogiFire.Core.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Published { get; set; }
    }
}