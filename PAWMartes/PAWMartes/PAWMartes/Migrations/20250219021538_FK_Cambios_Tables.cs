using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWMartes.Migrations
{
    /// <inheritdoc />
    public partial class FK_Cambios_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "UsuarioSistemas");

            migrationBuilder.RenameColumn(
                name: "Hora",
                table: "Evento",
                newName: "HoraEvento");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Evento",
                newName: "FechaEvento");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Evento",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "UsuarioSistemas",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "UsuarioSistemas",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Contraseña",
                table: "UsuarioSistemas",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioSistemas",
                table: "UsuarioSistemas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_EventoId",
                table: "Reserva",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_RESERVAS_USUARIO",
                table: "Reserva",
                column: "UsuarioId",
                principalTable: "UsuarioSistemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Evento_EventoId",
                table: "Reserva",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RESERVAS_USUARIO",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Evento_EventoId",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_EventoId",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioSistemas",
                table: "UsuarioSistemas");

            migrationBuilder.RenameTable(
                name: "UsuarioSistemas",
                newName: "Usuario");

            migrationBuilder.RenameColumn(
                name: "HoraEvento",
                table: "Evento",
                newName: "Hora");

            migrationBuilder.RenameColumn(
                name: "FechaEvento",
                table: "Evento",
                newName: "Fecha");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Evento",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Contraseña",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");
        }
    }
}
