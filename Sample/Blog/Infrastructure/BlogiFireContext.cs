using BlogiFire.Core.Data;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace BlogiFire.Web
{
    public class BlogiFireContext : DbContext
    {
        public BlogiFireContext() { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptions builder)
        {
            builder.UseSqlServer(AppSettings.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Blog>().ForRelational().Table("bf_blogs");
            builder.Entity<Post>().ForRelational().Table("bf_posts");

            base.OnModelCreating(builder);
        }
    }
}