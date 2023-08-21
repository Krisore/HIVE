using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIVE.Server.Migrations
{
    /// <inheritdoc />
    public partial class DocumentPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAgree",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgree",
                table: "Documents");
        }
    }
}
