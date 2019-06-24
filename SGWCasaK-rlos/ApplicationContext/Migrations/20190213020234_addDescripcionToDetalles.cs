using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addDescripcionToDetalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "DetallesVenta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "DetallesPedido",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "DetallesCompra",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "DetallesPedido");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "DetallesCompra");
        }
    }
}
