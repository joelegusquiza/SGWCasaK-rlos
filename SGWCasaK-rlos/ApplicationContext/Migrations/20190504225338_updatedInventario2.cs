using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class updatedInventario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockAnterior",
                table: "DetalleInventario",
                newName: "StockEncontrado");

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "Inventario",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockEncontrado",
                table: "DetalleInventario",
                newName: "StockAnterior");

            migrationBuilder.AlterColumn<int>(
                name: "SucursalId",
                table: "Inventario",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
