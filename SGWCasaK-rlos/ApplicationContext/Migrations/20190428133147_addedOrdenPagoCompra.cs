using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addedOrdenPagoCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagosCompras");

            migrationBuilder.CreateTable(
                name: "OrdenPagoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    ProveedorId = table.Column<int>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenPagoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenPagoCompra_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenPagoCompraDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CompraId = table.Column<int>(nullable: false),
                    OrdenPagoCompraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenPagoCompraDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenPagoCompraDetalle_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenPagoCompraDetalle_OrdenPagoCompra_OrdenPagoCompraId",
                        column: x => x.OrdenPagoCompraId,
                        principalTable: "OrdenPagoCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPagoCompra_ProveedorId",
                table: "OrdenPagoCompra",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPagoCompraDetalle_CompraId",
                table: "OrdenPagoCompraDetalle",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPagoCompraDetalle_OrdenPagoCompraId",
                table: "OrdenPagoCompraDetalle",
                column: "OrdenPagoCompraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenPagoCompraDetalle");

            migrationBuilder.DropTable(
                name: "OrdenPagoCompra");

            migrationBuilder.CreateTable(
                name: "PagosCompras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CompraId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Monto = table.Column<decimal>(nullable: false),
                    ProveedorId = table.Column<int>(nullable: true),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosCompras_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosCompras_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagosCompras_CompraId",
                table: "PagosCompras",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosCompras_ProveedorId",
                table: "PagosCompras",
                column: "ProveedorId");
        }
    }
}
