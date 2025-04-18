using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWMartes.Migrations
{
    /// <inheritdoc />
    public partial class AddFechaRegistro_Evento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fechaRegistro",
                table: "Evento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fechaRegistro",
                table: "Evento");
        }
    }
}
