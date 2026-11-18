using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavyArchives.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviourExperiment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandTours_Bands_BandId",
                table: "BandTours");

            migrationBuilder.DropForeignKey(
                name: "FK_BandTours_Tours_TourID",
                table: "BandTours");

            migrationBuilder.DropForeignKey(
                name: "FK_Concerts_Tours_TourId",
                table: "Concerts");

            migrationBuilder.AddForeignKey(
                name: "FK_BandTours_Bands_BandId",
                table: "BandTours",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BandTours_Tours_TourID",
                table: "BandTours",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Concerts_Tours_TourId",
                table: "Concerts",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandTours_Bands_BandId",
                table: "BandTours");

            migrationBuilder.DropForeignKey(
                name: "FK_BandTours_Tours_TourID",
                table: "BandTours");

            migrationBuilder.DropForeignKey(
                name: "FK_Concerts_Tours_TourId",
                table: "Concerts");

            migrationBuilder.AddForeignKey(
                name: "FK_BandTours_Bands_BandId",
                table: "BandTours",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BandTours_Tours_TourID",
                table: "BandTours",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Concerts_Tours_TourId",
                table: "Concerts",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
