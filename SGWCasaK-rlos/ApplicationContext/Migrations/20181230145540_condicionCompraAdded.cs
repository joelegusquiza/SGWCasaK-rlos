using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class condicionCompraAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CondicionCompra",
                table: "Compras",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicionCompra",
                table: "Compras");
        }
    }
}
