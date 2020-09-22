using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Database.Migrations.Migrations
{
    public partial class SecondIteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");
        }
    }
}
