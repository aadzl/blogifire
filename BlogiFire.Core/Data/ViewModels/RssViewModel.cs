using System;
using System.Collections.Generic;

namespace BlogiFire.Core.Data
{
    public class RssViewModel
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }       
        public List<PostView> Posts { get; set; }
    }

    public class PostView
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime Updated { get; set; }
    }
}