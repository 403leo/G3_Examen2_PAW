using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWMartes.Migrations
{
    /// <inheritdoc />
    public partial class CantReservas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Reserva",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Reserva");
        }
    }
}
