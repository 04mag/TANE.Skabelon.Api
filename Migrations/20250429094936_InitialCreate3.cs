using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TANE.Skabelon.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DagSkabelon_TurSkabelon_TurSkabelonId",
                table: "DagSkabelon");

            migrationBuilder.DropForeignKey(
                name: "FK_TurSkabelon_RejseplanSkabelon_RejseplanSkabelonId",
                table: "TurSkabelon");

            migrationBuilder.DropIndex(
                name: "IX_TurSkabelon_RejseplanSkabelonId",
                table: "TurSkabelon");

            migrationBuilder.DropIndex(
                name: "IX_DagSkabelon_TurSkabelonId",
                table: "DagSkabelon");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TurSkabelon");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TurSkabelon");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TurSkabelon");

            migrationBuilder.DropColumn(
                name: "RejseplanSkabelonId",
                table: "TurSkabelon");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TurSkabelon");

            migrationBuilder.DropColumn(
                name: "AntalDage",
                table: "RejseplanSkabelon");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RejseplanSkabelon");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RejseplanSkabelon");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RejseplanSkabelon");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RejseplanSkabelon");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DagSkabelon");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DagSkabelon");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DagSkabelon");

            migrationBuilder.DropColumn(
                name: "TurSkabelonId",
                table: "DagSkabelon");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DagSkabelon");

            migrationBuilder.AddColumn<int>(
                name: "RejseplanSkabelonModelId",
                table: "TurSkabelon",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurSkabelonModelId",
                table: "DagSkabelon",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TurSkabelon_RejseplanSkabelonModelId",
                table: "TurSkabelon",
                column: "RejseplanSkabelonModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DagSkabelon_TurSkabelonModelId",
                table: "DagSkabelon",
                column: "TurSkabelonModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DagSkabelon_TurSkabelon_TurSkabelonModelId",
                table: "DagSkabelon",
                column: "TurSkabelonModelId",
                principalTable: "TurSkabelon",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurSkabelon_RejseplanSkabelon_RejseplanSkabelonModelId",
                table: "TurSkabelon",
                column: "RejseplanSkabelonModelId",
                principalTable: "RejseplanSkabelon",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DagSkabelon_TurSkabelon_TurSkabelonModelId",
                table: "DagSkabelon");

            migrationBuilder.DropForeignKey(
                name: "FK_TurSkabelon_RejseplanSkabelon_RejseplanSkabelonModelId",
                table: "TurSkabelon");

            migrationBuilder.DropIndex(
                name: "IX_TurSkabelon_RejseplanSkabelonModelId",
                table: "TurSkabelon");

            migrationBuilder.DropIndex(
                name: "IX_DagSkabelon_TurSkabelonModelId",
                table: "DagSkabelon");

            migrationBuilder.DropColumn(
                name: "RejseplanSkabelonModelId",
                table: "TurSkabelon");

            migrationBuilder.DropColumn(
                name: "TurSkabelonModelId",
                table: "DagSkabelon");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TurSkabelon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TurSkabelon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TurSkabelon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RejseplanSkabelonId",
                table: "TurSkabelon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TurSkabelon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AntalDage",
                table: "RejseplanSkabelon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RejseplanSkabelon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RejseplanSkabelon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RejseplanSkabelon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RejseplanSkabelon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DagSkabelon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DagSkabelon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DagSkabelon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TurSkabelonId",
                table: "DagSkabelon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DagSkabelon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TurSkabelon_RejseplanSkabelonId",
                table: "TurSkabelon",
                column: "RejseplanSkabelonId");

            migrationBuilder.CreateIndex(
                name: "IX_DagSkabelon_TurSkabelonId",
                table: "DagSkabelon",
                column: "TurSkabelonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DagSkabelon_TurSkabelon_TurSkabelonId",
                table: "DagSkabelon",
                column: "TurSkabelonId",
                principalTable: "TurSkabelon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurSkabelon_RejseplanSkabelon_RejseplanSkabelonId",
                table: "TurSkabelon",
                column: "RejseplanSkabelonId",
                principalTable: "RejseplanSkabelon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
