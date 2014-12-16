using BlogiFire.Web;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace Sample.Migrations
{
    [ContextType(typeof(BlogiFireContext))]
    public partial class bf01 : IMigrationMetadata
    {
        string IMigrationMetadata.MigrationId
        {
            get
            {
                return "201412131823444_bf01";
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
                
                builder.Entity("BlogiFire.Core.Data.Blog", b =>
                    {
                        b.Property<string>("AuthorEmail");
                        b.Property<string>("AuthorId");
                        b.Property<string>("AuthorName");
                        b.Property<string>("CoverImage");
                        b.Property<int>("DaysToComment");
                        b.Property<string>("Description");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<bool>("IsModerated");
                        b.Property<bool>("IsSelected");
                        b.Property<string>("LogoImage");
                        b.Property<int>("PostsPerPage");
                        b.Property<string>("ProfileImage");
                        b.Property<string>("Theme");
                        b.Property<string>("Title");
                        b.Key("Id");
                        b.ForRelational().Table("bf_blogs");
                    });
                
                builder.Entity("BlogiFire.Core.Data.Comment", b =>
                    {
                        b.Property<string>("Author");
                        b.Property<string>("Content");
                        b.Property<string>("Email");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<string>("Ip");
                        b.Property<bool>("IsApproved");
                        b.Property<int>("ParentId");
                        b.Property<int>("PostId");
                        b.Property<DateTime>("Published");
                        b.Property<string>("UserAgent");
                        b.Property<string>("Website");
                        b.Key("Id");
                        b.ForRelational().Table("bf_comments");
                    });
                
                builder.Entity("BlogiFire.Core.Data.Post", b =>
                    {
                        b.Property<string>("AuthorName");
                        b.Property<int>("BlogId");
                        b.Property<int>("Comments");
                        b.Property<bool>("CommentsEnabled");
                        b.Property<string>("Content");
                        b.Property<int>("Id")
                            .GenerateValuesOnAdd();
                        b.Property<bool>("IsSelected");
                        b.Property<DateTime>("Published");
                        b.Property<DateTime>("Saved");
                        b.Property<string>("Slug");
                        b.Property<string>("Tags");
                        b.Property<string>("Title");
                        b.Key("Id");
                        b.ForRelational().Table("bf_posts");
                    });
                
                return builder.Model;
            }
        }
    }
}