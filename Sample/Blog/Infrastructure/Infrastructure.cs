using BlogiFire.Core.Data;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace BlogiFire.Web
{
    public static class Infrastructure
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            try
            {
                var config = new Configuration().AddJsonFile("config.json").AddEnvironmentVariables();
                AppSettings.ConnectionString = config.Get("Data:DefaultConnection:ConnectionString");
            }
            catch (Exception)
            {
                AppSettings.ConnectionString = "Server=.\\SQLEXPRESS;Database=BlogiFire;Trusted_Connection=True;MultipleActiveResultSets=true";
            }

            // true to seed test data
            AppSettings.InitializeData = true;

            using (var db = new BlogiFireContext())
            {
                db.Database.AsRelational().ApplyMigrations();
                db.Database.EnsureCreatedAsync();
            }

            if (AppSettings.InitializeData)
            {
                var initializer = new DataInitializer();
                initializer.SeedBlogs();
            }
        }
    }
}