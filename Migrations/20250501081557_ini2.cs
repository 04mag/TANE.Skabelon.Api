using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TANE.Skabelon.Api.Migrations
{
    /// <inheritdoc />
    public partial class ini2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beskrivelse",
                table: "RejseplanSkabelon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "RejseplanSkabelon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beskrivelse",
                table: "RejseplanSkabelon");

            migrationBuilder.DropColumn(
                name: "Titel",
                table: "RejseplanSkabelon");
        }
    }
}
