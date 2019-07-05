using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addEquivalenciaInDetalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "ProductoPresentaciones",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Equivalencia",
                table: "DetallesVenta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Equivalencia",
                table: "DetallesPedido",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Equivalencia",
                table: "DetallesCompra",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "ProductoPresentaciones");

            migrationBuilder.DropColumn(
                name: "Equivalencia",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "Equivalencia",
                table: "DetallesPedido");

            migrationBuilder.DropColumn(
                name: "Equivalencia",
                table: "DetallesCompra");
        }
    }
}
