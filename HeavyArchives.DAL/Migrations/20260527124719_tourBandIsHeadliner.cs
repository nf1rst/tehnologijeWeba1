using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavyArchives.DAL.Migrations
{
    /// <inheritdoc />
    public partial class tourBandIsHeadliner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bands_Tours_TourId",
                table: "Bands");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Bands_HeadlinerBandId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_HeadlinerBandId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Bands_TourId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "HeadlinerBandId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Bands");

            migrationBuilder.AddColumn<bool>(
                name: "IsHeadliner",
                table: "BandTours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHeadliner",
                table: "BandTours");

            migrationBuilder.AddColumn<int>(
                name: "HeadlinerBandId",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "Bands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_HeadlinerBandId",
                table: "Tours",
                column: "HeadlinerBandId");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_TourId",
                table: "Bands",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_Tours_TourId",
                table: "Bands",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Bands_HeadlinerBandId",
                table: "Tours",
                column: "HeadlinerBandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
