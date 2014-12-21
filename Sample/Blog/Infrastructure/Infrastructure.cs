using BlogiFire.Core.Data;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System;

namespace BlogiFire.Web
{
    public static class Infrastructure
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            LoadConfiguration();

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

        static void LoadConfiguration()
        {
            // seed test data
            AppSettings.InitializeData = true;
            
            var bfConfig = new Configuration().AddJsonFile("blog/config.json").AddEnvironmentVariables();
            
            if (bfConfig != null)
            {              
                AppSettings.RelativeUrl = bfConfig.Get("BlogiFire:RelativeRoot");
                AppSettings.AbsoluteUrl = bfConfig.Get("BlogiFire:AbsoluteRoot");
                AppSettings.Title = bfConfig.Get("BlogiFire:Title");
                AppSettings.Description = bfConfig.Get("BlogiFire:Description");
                AppSettings.PageSize = int.Parse(bfConfig.Get("BlogiFire:PageSize"));
                if (!GetAppConnectionString())
                {
                    AppSettings.ConnectionString = bfConfig.Get("BlogiFire:ConnectionString");
                }
            }
        }
        static bool GetAppConnectionString()
        {
            try
            {
                var config = new Configuration().AddJsonFile("config.json").AddEnvironmentVariables();
                AppSettings.ConnectionString = config.Get("Data:DefaultConnection:ConnectionString");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}