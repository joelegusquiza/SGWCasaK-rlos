using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addedCajaIdInRecibos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CajaId",
                table: "Recibos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recibos_CajaId",
                table: "Recibos",
                column: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recibos_Cajas_CajaId",
                table: "Recibos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recibos_Cajas_CajaId",
                table: "Recibos");

            migrationBuilder.DropIndex(
                name: "IX_Recibos_CajaId",
                table: "Recibos");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Recibos");
        }
    }
}
