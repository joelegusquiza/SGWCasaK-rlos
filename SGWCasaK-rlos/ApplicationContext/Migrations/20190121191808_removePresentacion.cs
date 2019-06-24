using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class removePresentacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoPresentaciones_Presentaciones_ProductoId",
                table: "ProductoPresentaciones");

            migrationBuilder.DropTable(
                name: "Presentaciones");

            migrationBuilder.DropColumn(
                name: "PresentacionId",
                table: "ProductoPresentaciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PresentacionId",
                table: "ProductoPresentaciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Presentaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentaciones", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoPresentaciones_Presentaciones_ProductoId",
                table: "ProductoPresentaciones",
                column: "ProductoId",
                principalTable: "Presentaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
