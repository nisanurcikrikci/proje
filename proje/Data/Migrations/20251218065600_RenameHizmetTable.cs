using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameHizmetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerHizmet_Hizmet_HizmetId",
                table: "TrainerHizmet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hizmet",
                table: "Hizmet");

            migrationBuilder.RenameTable(
                name: "Hizmet",
                newName: "Hizmetler");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hizmetler",
                table: "Hizmetler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerHizmet_Hizmetler_HizmetId",
                table: "TrainerHizmet",
                column: "HizmetId",
                principalTable: "Hizmetler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerHizmet_Hizmetler_HizmetId",
                table: "TrainerHizmet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hizmetler",
                table: "Hizmetler");

            migrationBuilder.RenameTable(
                name: "Hizmetler",
                newName: "Hizmet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hizmet",
                table: "Hizmet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerHizmet_Hizmet_HizmetId",
                table: "TrainerHizmet",
                column: "HizmetId",
                principalTable: "Hizmet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
