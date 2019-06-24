using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class updateCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RUC",
                table: "Clientes",
                newName: "Ruc");

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "Clientes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Ruc",
                table: "Clientes",
                newName: "RUC");
        }
    }
}
