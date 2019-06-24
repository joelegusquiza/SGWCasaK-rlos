using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addCajaEntitie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CajaId",
                table: "Timbrados",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntoExpedicion",
                table: "Timbrados",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cajas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cajas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timbrados_CajaId",
                table: "Timbrados",
                column: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timbrados_Cajas_CajaId",
                table: "Timbrados",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timbrados_Cajas_CajaId",
                table: "Timbrados");

            migrationBuilder.DropTable(
                name: "Cajas");

            migrationBuilder.DropIndex(
                name: "IX_Timbrados_CajaId",
                table: "Timbrados");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Timbrados");

            migrationBuilder.DropColumn(
                name: "PuntoExpedicion",
                table: "Timbrados");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
