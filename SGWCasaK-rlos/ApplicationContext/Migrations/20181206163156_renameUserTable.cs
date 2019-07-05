using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class renameUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InternalBlogs",
                table: "InternalBlogs");

            migrationBuilder.RenameTable(
                name: "InternalBlogs",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "InternalBlogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternalBlogs",
                table: "InternalBlogs",
                column: "Id");
        }
    }
}
