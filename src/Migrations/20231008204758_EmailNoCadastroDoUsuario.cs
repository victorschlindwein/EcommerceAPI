using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuloAPI.Migrations
{
    /// <inheritdoc />
    public partial class EmailNoCadastroDoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contatos");
        }
    }
}
