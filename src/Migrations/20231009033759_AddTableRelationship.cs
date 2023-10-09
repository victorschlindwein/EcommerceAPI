using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuloAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContatoId",
                table: "Enderecos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ContatoId",
                table: "Enderecos",
                column: "ContatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Contatos_ContatoId",
                table: "Enderecos",
                column: "ContatoId",
                principalTable: "Contatos",
                principalColumn: "ContatoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Contatos_ContatoId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_ContatoId",
                table: "Enderecos");

            migrationBuilder.AlterColumn<string>(
                name: "ContatoId",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
