using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIVE.Server.Migrations
{
    /// <inheritdoc />
    public partial class IsConfirmedForGrammarAndStatisctic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Documents",
                newName: "IsConfirmedForGrammarAndStatistic");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmForPlagiarism",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmForPlagiarism",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "IsConfirmedForGrammarAndStatistic",
                table: "Documents",
                newName: "IsConfirmed");
        }
    }
}
