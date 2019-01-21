using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addpresentacionunidadmedida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadMedidaId",
                table: "Productos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductoPresentaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Equivalencia = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoPresentaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoPresentaciones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UnidadMedidaId",
                table: "Productos",
                column: "UnidadMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPresentaciones_ProductoId",
                table: "ProductoPresentaciones",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_UnidadesMedida_UnidadMedidaId",
                table: "Productos",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_UnidadesMedida_UnidadMedidaId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "ProductoPresentaciones");

            migrationBuilder.DropIndex(
                name: "IX_Productos_UnidadMedidaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UnidadMedidaId",
                table: "Productos");
        }
    }
}
