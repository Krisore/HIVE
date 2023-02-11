using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIVE.Server.Migrations
{
    /// <inheritdoc />
    public partial class FileEntry3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntries_Documents_DocumentId",
                table: "FileEntries");

            migrationBuilder.DropIndex(
                name: "IX_FileEntries_DocumentId",
                table: "FileEntries");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "FileEntries");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FileId",
                table: "Documents",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_FileEntries_FileId",
                table: "Documents",
                column: "FileId",
                principalTable: "FileEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_FileEntries_FileId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_FileId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "FileEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileEntries_DocumentId",
                table: "FileEntries",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntries_Documents_DocumentId",
                table: "FileEntries",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
