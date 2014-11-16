using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.SqlServer;
using BlogiFire.Core.Data;

namespace BlogiFire.Web.Models
{
    public class BlogModels : DbContext
    {
        private static bool _created = false;

        public DbSet<Post> Posts { get; set; }

        public BlogModels()
        {
            // Create the database and schema if it doesn't exist
            // This is a temporary workaround to create database until Entity Framework database migrations 
            // are supported in ASP.NET 5
            if (!_created)
            {
                Database.AsRelational().ApplyMigrations();
                _created = true;
            }
        }

        protected override void OnConfiguring(DbContextOptions builder)
        {
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BlogiFire;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}