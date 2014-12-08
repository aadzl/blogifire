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
            
            migrationBuilder.CreateTable("bf_posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        BlogId = c.Int(nullable: false),
                        Comments = c.Int(nullable: false),
                        CommentsEnabled = c.Boolean(nullable: false),
                        Content = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                        Published = c.DateTime(nullable: false),
                        Saved = c.DateTime(nullable: false),
                        Slug = c.String(),
                        Tags = c.String(),
                        Title = c.String()
                    })
                .PrimaryKey("PK_bf_posts", t => t.Id);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("bf_blogs");
            
            migrationBuilder.DropTable("bf_posts");
        }
    }
}