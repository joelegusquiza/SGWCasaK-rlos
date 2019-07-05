using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addedCajaAperturaCierreDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleCajaAperturaCierre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    TipoOperacion = table.Column<int>(nullable: false),
                    Monto = table.Column<decimal>(nullable: false),
                    Cambio = table.Column<decimal>(nullable: false),
                    CajaAperturaCierreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCajaAperturaCierre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCajaAperturaCierre_CajaAperturaCierre_CajaAperturaCierreId",
                        column: x => x.CajaAperturaCierreId,
                        principalTable: "CajaAperturaCierre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCajaAperturaCierre_CajaAperturaCierreId",
                table: "DetalleCajaAperturaCierre",
                column: "CajaAperturaCierreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCajaAperturaCierre");
        }
    }
}
