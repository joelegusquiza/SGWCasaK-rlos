using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addTipoProveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Proveedores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Proveedores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Proveedores");
        }
    }
}
