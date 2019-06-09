using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class movePuntoExpedicion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntoExpedicion",
                table: "Timbrados");

            migrationBuilder.AddColumn<int>(
                name: "PuntoExpedicion",
                table: "Cajas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntoExpedicion",
                table: "Cajas");

            migrationBuilder.AddColumn<int>(
                name: "PuntoExpedicion",
                table: "Timbrados",
                nullable: false,
                defaultValue: 0);
        }
    }
}
