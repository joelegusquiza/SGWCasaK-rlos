using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addProcentajeGanancia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "ProductoPresentaciones");

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeGanancia",
                table: "Productos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeGanancia",
                table: "ProductoPresentaciones",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeGanancia",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PorcentajeGanancia",
                table: "ProductoPresentaciones");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "Productos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "ProductoPresentaciones",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
