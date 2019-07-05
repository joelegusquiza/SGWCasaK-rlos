using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class updateInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazonAnulado",
                table: "Inventario",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFinId",
                table: "Inventario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInicioId",
                table: "Inventario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_UsuarioFinId",
                table: "Inventario",
                column: "UsuarioFinId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_UsuarioInicioId",
                table: "Inventario",
                column: "UsuarioInicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Usuarios_UsuarioFinId",
                table: "Inventario",
                column: "UsuarioFinId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Usuarios_UsuarioInicioId",
                table: "Inventario",
                column: "UsuarioInicioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Usuarios_UsuarioFinId",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Usuarios_UsuarioInicioId",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_UsuarioFinId",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_UsuarioInicioId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "RazonAnulado",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "UsuarioFinId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "UsuarioInicioId",
                table: "Inventario");
        }
    }
}
