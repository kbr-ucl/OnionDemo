using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Database.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Blogs",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Blogs", x => x.Id); });

            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BlogId = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        "FK_Posts_Blogs_BlogId",
                        x => x.BlogId,
                        "Blogs",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Posts_BlogId",
                "Posts",
                "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Posts");

            migrationBuilder.DropTable(
                "Blogs");
        }
    }
}