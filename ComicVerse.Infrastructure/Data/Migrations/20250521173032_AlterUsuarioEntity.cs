using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicVerse.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterUsuarioEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Usuarios",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
