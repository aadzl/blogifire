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
            services.AddSingleton<Core.Data.IBlogRepository, Core.Data.BlogRepository>();
            services.AddSingleton<Core.Data.IPostRepository, Core.Data.PostRepository>();

            try
            {
                var config = new Configuration().AddJsonFile("config.json").AddEnvironmentVariables();
                Core.Data.AppSettings.ConnectionString = config.Get("Data:DefaultConnection:ConnectionString");
            }
            catch (Exception)
            {
                Core.Data.AppSettings.ConnectionString = "Server=.\\SQLEXPRESS;Database=BlogiFire;Trusted_Connection=True;MultipleActiveResultSets=true";
            }

            // if true, for every created blog seed posts
            Core.Data.AppSettings.InitializeData = true;

            using (var db = new BlogiFireContext())
            {
                db.Database.AsRelational().ApplyMigrations();
                db.Database.EnsureCreatedAsync();
            }
        }
    }
}