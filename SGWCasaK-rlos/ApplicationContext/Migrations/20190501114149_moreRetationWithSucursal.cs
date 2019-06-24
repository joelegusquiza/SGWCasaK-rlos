using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class moreRetationWithSucursal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "OrdenPagoCompra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Inventario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPagoCompra_SucursalId",
                table: "OrdenPagoCompra",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_SucursalId",
                table: "Inventario",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Sucursales_SucursalId",
                table: "Inventario",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenPagoCompra_Sucursales_SucursalId",
                table: "OrdenPagoCompra",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Sucursales_SucursalId",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenPagoCompra_Sucursales_SucursalId",
                table: "OrdenPagoCompra");

            migrationBuilder.DropIndex(
                name: "IX_OrdenPagoCompra_SucursalId",
                table: "OrdenPagoCompra");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_SucursalId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "OrdenPagoCompra");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Inventario");
        }
    }
}
