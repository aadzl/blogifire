using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Model;
using System;

namespace WebApp.Migrations
{
    public partial class bf01 : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentsCount = c.Int(nullable: false),
                        Content = c.String(),
                        Published = c.Boolean(nullable: false),
                        Title = c.String()
                    })
                .PrimaryKey("PK_Post", t => t.Id);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Post");
        }
    }
}