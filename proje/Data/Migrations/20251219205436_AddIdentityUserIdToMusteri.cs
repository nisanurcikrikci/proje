using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserIdToMusteri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Musteriler");
        }
    }
}
