using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavyArchives.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Persons_TourManagerId",
                table: "Tours");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Persons_TourManagerId",
                table: "Tours",
                column: "TourManagerId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Persons_TourManagerId",
                table: "Tours");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Persons_TourManagerId",
                table: "Tours",
                column: "TourManagerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
