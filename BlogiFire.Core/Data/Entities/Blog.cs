using System;

namespace BlogiFire.Core.Data
{
    public class Blog
    {
        public Blog()
        {
            PostsPerPage = 10;
            DaysToComment = 0;
            IsModerated = false;
            Theme = "standard";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool IsModerated { get; set; }
        public string Theme { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string LogoImage { get; set; }
        public bool IsSelected { get; set; }
    }
}