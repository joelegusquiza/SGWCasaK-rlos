using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addedMontoInOrdenPagoCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Monto",
                table: "OrdenPagoCompraDetalle",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoTotal",
                table: "OrdenPagoCompra",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monto",
                table: "OrdenPagoCompraDetalle");

            migrationBuilder.DropColumn(
                name: "MontoTotal",
                table: "OrdenPagoCompra");
        }
    }
}
