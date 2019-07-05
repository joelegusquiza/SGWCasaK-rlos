using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class updateCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProveedorId",
                table: "PagosCompras",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Compras",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "ProveedorId",
                table: "PagosCompras",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
