using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIVE.Server.Migrations
{
    /// <inheritdoc />
    public partial class BadRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentHistories_Documents_DocumentId",
                table: "DocumentHistories");

            migrationBuilder.DropIndex(
                name: "IX_DocumentHistories_DocumentId",
                table: "DocumentHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DocumentHistories_DocumentId",
                table: "DocumentHistories",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentHistories_Documents_DocumentId",
                table: "DocumentHistories",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
