using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWMartes.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioId_Evento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Evento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evento_UsuarioId",
                table: "Evento",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Usuario_UsuarioId",
                table: "Evento",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Usuario_UsuarioId",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_UsuarioId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Evento");
        }
    }
}
