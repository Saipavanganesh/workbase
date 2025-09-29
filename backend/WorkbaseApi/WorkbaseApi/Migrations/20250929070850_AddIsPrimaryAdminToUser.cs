using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkbaseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPrimaryAdminToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryAdmin",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryAdmin",
                table: "Users");
        }
    }
}
