using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hizmet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SureDakika = table.Column<int>(type: "int", nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzmanlik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzmanlik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerHizmet",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerHizmet", x => new { x.TrainerId, x.HizmetId });
                    table.ForeignKey(
                        name: "FK_TrainerHizmet_Hizmet_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerHizmet_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerUzmanlik",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    UzmanlikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerUzmanlik", x => new { x.TrainerId, x.UzmanlikId });
                    table.ForeignKey(
                        name: "FK_TrainerUzmanlik_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerUzmanlik_Uzmanlik_UzmanlikId",
                        column: x => x.UzmanlikId,
                        principalTable: "Uzmanlik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hizmet",
                columns: new[] { "Id", "Ad", "SureDakika", "Ucret" },
                values: new object[,]
                {
                    { 1, "Fitness", 60, 300m },
                    { 2, "Yoga", 60, 250m },
                    { 3, "Pilates", 60, 280m },
                    { 4, "CrossFit", 45, 320m }
                });

            migrationBuilder.InsertData(
                table: "Uzmanlik",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Kas Geliştirme" },
                    { 2, "Kilo Verme" },
                    { 3, "Yoga" },
                    { 4, "Pilates" },
                    { 5, "CrossFit" }
                });

            migrationBuilder.InsertData(
                table: "TrainerHizmet",
                columns: new[] { "HizmetId", "TrainerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 4, 3 },
                    { 2, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "TrainerUzmanlik",
                columns: new[] { "TrainerId", "UzmanlikId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 5 },
                    { 4, 3 },
                    { 5, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerHizmet_HizmetId",
                table: "TrainerHizmet",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerUzmanlik_UzmanlikId",
                table: "TrainerUzmanlik",
                column: "UzmanlikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerHizmet");

            migrationBuilder.DropTable(
                name: "TrainerUzmanlik");

            migrationBuilder.DropTable(
                name: "Hizmet");

            migrationBuilder.DropTable(
                name: "Uzmanlik");
        }
    }
}
