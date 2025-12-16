using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "AdSoyad", "AktifMi" },
                values: new object[,]
                {
                    { 1, "Ahmet Yılmaz", true },
                    { 2, "Elif Kaya", true },
                    { 3, "Mert Demir", true },
                    { 4, "Zeynep Arslan", true },
                    { 5, "Can Öztürk", true },
                    { 6, "Ayşe Çelik", true },
                    { 7, "Burak Koç", true },
                    { 8, "Derya Şahin", true },
                    { 9, "Emre Aydın", true },
                    { 10, "Selin Karaca", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
