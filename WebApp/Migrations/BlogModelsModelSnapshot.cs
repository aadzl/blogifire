using BlogiFire.Web.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace WebApp.Migrations
{
    [ContextType(typeof(BlogModels))]
    public class BlogModelsModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("BlogiFire.Core.Data.Post", b =>
                    {
                        b.Property<int>("CommentsCount");
                        b.Property<string>("Content");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<bool>("Published");
                        b.Property<string>("Title");
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}