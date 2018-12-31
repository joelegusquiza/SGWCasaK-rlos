using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addCambioToVentaCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cambio",
                table: "Ventas",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cambio",
                table: "Compras",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cambio",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Cambio",
                table: "Compras");
        }
    }
}
