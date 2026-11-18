using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavyArchives.DAL.Migrations
{
    /// <inheritdoc />
    public partial class userFavBand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteBand",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteBand",
                table: "Users");
        }
    }
}
