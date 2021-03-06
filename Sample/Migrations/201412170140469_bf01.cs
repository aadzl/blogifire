using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Model;
using System;

namespace Sample.Migrations
{
    public partial class bf01 : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("bf_blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorEmail = c.String(),
                        AuthorId = c.String(),
                        AuthorName = c.String(),
                        CoverImage = c.String(),
                        DaysToComment = c.Int(nullable: false),
                        Description = c.String(),
                        IsModerated = c.Boolean(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        LogoImage = c.String(),
                        PostsPerPage = c.Int(nullable: false),
                        ProfileImage = c.String(),
                        Theme = c.String(),
                        Title = c.String()
                    })
                .PrimaryKey("PK_bf_blogs", t => t.Id);
            
            migrationBuilder.CreateTable("bf_comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Content = c.String(),
                        Email = c.String(),
                        Ip = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        ParentId = c.Int(nullable: false),
                        Published = c.DateTime(nullable: false),
                        UserAgent = c.String(),
                        Website = c.String(),
                        PostId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_bf_comments", t => t.Id);
            
            migrationBuilder.CreateTable("bf_posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        Comments = c.Int(nullable: false),
                        CommentsEnabled = c.Boolean(nullable: false),
                        Content = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                        Published = c.DateTime(nullable: false),
                        Saved = c.DateTime(nullable: false),
                        Slug = c.String(),
                        Tags = c.String(),
                        Title = c.String(),
                        BlogId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_bf_posts", t => t.Id);
            
            migrationBuilder.AddForeignKey("bf_comments", "FK_bf_comments_bf_posts_PostId", new[] { "PostId" }, "bf_posts", new[] { "Id" }, cascadeDelete: false);
            
            migrationBuilder.AddForeignKey("bf_posts", "FK_bf_posts_bf_blogs_BlogId", new[] { "BlogId" }, "bf_blogs", new[] { "Id" }, cascadeDelete: false);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("bf_comments", "FK_bf_comments_bf_posts_PostId");
            
            migrationBuilder.DropForeignKey("bf_posts", "FK_bf_posts_bf_blogs_BlogId");
            
            migrationBuilder.DropTable("bf_blogs");
            
            migrationBuilder.DropTable("bf_comments");
            
            migrationBuilder.DropTable("bf_posts");
        }
    }
}