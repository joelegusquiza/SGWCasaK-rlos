using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addIsClienteToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Usuarios");

            migrationBuilder.AddColumn<bool>(
                name: "IsCliente",
                table: "Roles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCliente",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Usuarios",
                nullable: true);
        }
    }
}
