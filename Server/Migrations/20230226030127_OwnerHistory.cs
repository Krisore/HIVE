using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIVE.Server.Migrations
{
    /// <inheritdoc />
    public partial class OwnerHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "DocumentHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "DocumentHistories");
        }
    }
}
