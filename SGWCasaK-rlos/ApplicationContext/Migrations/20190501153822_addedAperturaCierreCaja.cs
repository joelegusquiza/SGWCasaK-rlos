using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class addedAperturaCierreCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CajaAperturaCierre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserCreatedId = table.Column<int>(nullable: false),
                    UserModifiedId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    FechaApertura = table.Column<DateTimeOffset>(nullable: true),
                    FechaCierre = table.Column<DateTimeOffset>(nullable: true),
                    MontoApertura = table.Column<decimal>(nullable: false),
                    MontoCierre = table.Column<decimal>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    CajaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CajaAperturaCierre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CajaAperturaCierre_Cajas_CajaId",
                        column: x => x.CajaId,
                        principalTable: "Cajas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CajaAperturaCierre_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CajaAperturaCierre_CajaId",
                table: "CajaAperturaCierre",
                column: "CajaId");

            migrationBuilder.CreateIndex(
                name: "IX_CajaAperturaCierre_UsuarioId",
                table: "CajaAperturaCierre",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CajaAperturaCierre");
        }
    }
}
