using System;
using Microsoft.Data.Entity;
using BlogiFire.Core.Data.Entities;

namespace BlogiFire.Web.Areas.Blog.Models
{
    public class BlogiFireContext : DbContext
    {
        public BlogiFireContext()
        {
            Core.Services.Logging.Logger.Log("Empty constructor");
        }

        public BlogiFireContext(DbContextOptions options) : base(options)
        {
            Core.Services.Logging.Logger.Log("Constractor with options");
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptions options)
        {
            Core.Services.Logging.Logger.Log("On confituration");

            options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=BlogiFire;integrated security=True;");

            Init();
        }

        protected void Init()
        {
            // for development only
            // drop and recreate DB on every run
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}