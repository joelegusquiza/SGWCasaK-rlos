using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class removedStockFromProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "ProductoSucursal",
                newName: "Stock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "ProductoSucursal",
                newName: "Cantidad");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
