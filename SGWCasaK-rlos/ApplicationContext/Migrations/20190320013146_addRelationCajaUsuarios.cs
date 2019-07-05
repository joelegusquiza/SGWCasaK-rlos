using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addRelationCajaUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CajaId",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CajaId",
                table: "Usuarios",
                column: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cajas_CajaId",
                table: "Usuarios",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cajas_CajaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_CajaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Usuarios");
        }
    }
}
