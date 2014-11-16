using BlogiFire.Web.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace WebApp.Migrations
{
    [ContextType(typeof(BlogModels))]
    public partial class bf01 : IMigrationMetadata
    {
        string IMigrationMetadata.MigrationId
        {
            get
            {
                return "201411151728599_bf01";
            }
        }
        
        string IMigrationMetadata.ProductVersion
        {
            get
            {
                return "7.0.0-beta1-11518";
            }
        }
        
        IModel IMigrationMetadata.TargetModel
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