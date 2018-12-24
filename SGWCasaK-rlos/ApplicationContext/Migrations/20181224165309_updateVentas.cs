using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class updateVentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Exenta",
                table: "Ventas",
                newName: "Excenta");

            migrationBuilder.AddColumn<int>(
                name: "CondicionVenta",
                table: "Ventas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicionVenta",
                table: "Ventas");

            migrationBuilder.RenameColumn(
                name: "Excenta",
                table: "Ventas",
                newName: "Exenta");
        }
    }
}
