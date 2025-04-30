using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TANE.Skabelon.Api.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DagSkabelon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RejseplanSkabelon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
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
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurSkabelon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DagSkabelonModelTurSkabelonModel",
                columns: table => new
                {
                    DagSkabelonerId = table.Column<int>(type: "int", nullable: false),
                    TurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DagSkabelonModelTurSkabelonModel", x => new { x.DagSkabelonerId, x.TurId });
                    table.ForeignKey(
                        name: "FK_DagSkabelonModelTurSkabelonModel_DagSkabelon_DagSkabelonerId",
                        column: x => x.DagSkabelonerId,
                        principalTable: "DagSkabelon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DagSkabelonModelTurSkabelonModel_TurSkabelon_TurId",
                        column: x => x.TurId,
                        principalTable: "TurSkabelon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RejseplanSkabelonModelTurSkabelonModel",
                columns: table => new
                {
                    RejseplanSkabelonerId = table.Column<int>(type: "int", nullable: false),
                    TurSkabelonerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejseplanSkabelonModelTurSkabelonModel", x => new { x.RejseplanSkabelonerId, x.TurSkabelonerId });
                    table.ForeignKey(
                        name: "FK_RejseplanSkabelonModelTurSkabelonModel_RejseplanSkabelon_RejseplanSkabelonerId",
                        column: x => x.RejseplanSkabelonerId,
                        principalTable: "RejseplanSkabelon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RejseplanSkabelonModelTurSkabelonModel_TurSkabelon_TurSkabelonerId",
                        column: x => x.TurSkabelonerId,
                        principalTable: "TurSkabelon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DagSkabelonModelTurSkabelonModel_TurId",
                table: "DagSkabelonModelTurSkabelonModel",
                column: "TurId");

            migrationBuilder.CreateIndex(
                name: "IX_RejseplanSkabelonModelTurSkabelonModel_TurSkabelonerId",
                table: "RejseplanSkabelonModelTurSkabelonModel",
                column: "TurSkabelonerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DagSkabelonModelTurSkabelonModel");

            migrationBuilder.DropTable(
                name: "RejseplanSkabelonModelTurSkabelonModel");

            migrationBuilder.DropTable(
                name: "DagSkabelon");

            migrationBuilder.DropTable(
                name: "RejseplanSkabelon");

            migrationBuilder.DropTable(
                name: "TurSkabelon");
        }
    }
}
