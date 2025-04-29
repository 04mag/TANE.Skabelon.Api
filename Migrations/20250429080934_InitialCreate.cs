using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TANE.Skabelon.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RejseplanSkabelon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AntalDage = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejseplanSkabelon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurSkabelon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejseplanSkabelonId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurSkabelon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurSkabelon_RejseplanSkabelon_RejseplanSkabelonId",
                        column: x => x.RejseplanSkabelonId,
                        principalTable: "RejseplanSkabelon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DagSkabelon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktiviteter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Måltider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overnatning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<double>(type: "float", nullable: false),
                    TurSkabelonId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DagSkabelon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DagSkabelon_TurSkabelon_TurSkabelonId",
                        column: x => x.TurSkabelonId,
                        principalTable: "TurSkabelon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DagSkabelon_TurSkabelonId",
                table: "DagSkabelon",
                column: "TurSkabelonId");

            migrationBuilder.CreateIndex(
                name: "IX_TurSkabelon_RejseplanSkabelonId",
                table: "TurSkabelon",
                column: "RejseplanSkabelonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DagSkabelon");

            migrationBuilder.DropTable(
                name: "TurSkabelon");

            migrationBuilder.DropTable(
                name: "RejseplanSkabelon");
        }
    }
}
