using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationContext.Migrations
{
    public partial class updateDateCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCompra",
                table: "Compras");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCompra",
                table: "Compras",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompra",
                table: "Compras");

            migrationBuilder.AddColumn<decimal>(
                name: "FechaCompra",
                table: "Compras",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
