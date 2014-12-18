using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace BlogiFire.Core.Data
{
    public class BlogiFireContext : DbContext
    {
        public BlogiFireContext() { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptions builder)
        {
            if (AppSettings.UseInMemoryDb)
            {
                builder.UseInMemoryStore();
            }
            else
            {
                builder.UseSqlServer(AppSettings.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Blog>().ForRelational().Table("bf_blogs");
            builder.Entity<Post>().ForRelational().Table("bf_posts");
            builder.Entity<Comment>().ForRelational().Table("bf_comments");

            builder.Entity<Post>().ForeignKey<Blog>(p => p.BlogId);
            builder.Entity<Comment>().ForeignKey<Post>(c => c.PostId);

            base.OnModelCreating(builder);
        }
    }
}