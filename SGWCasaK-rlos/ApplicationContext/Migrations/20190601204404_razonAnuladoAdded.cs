using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class razonAnuladoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazonAnulado",
                table: "Ventas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazonAnulado",
                table: "Pedidos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazonAnulado",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "RazonAnulado",
                table: "Pedidos");
        }
    }
}
