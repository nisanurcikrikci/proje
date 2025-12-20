using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrainerProfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Boy",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Kilo",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_IdentityUserId",
                table: "Trainers",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_IdentityUserId",
                table: "Trainers",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_IdentityUserId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_IdentityUserId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Boy",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Kilo",
                table: "Trainers");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
