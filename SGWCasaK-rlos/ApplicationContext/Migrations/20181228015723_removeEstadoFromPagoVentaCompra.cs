using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class removeEstadoFromPagoVentaCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "PagosVentas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "PagosCompras");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Ventas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Ventas");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "PagosVentas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "PagosCompras",
                nullable: false,
                defaultValue: 0);
        }
    }
}
