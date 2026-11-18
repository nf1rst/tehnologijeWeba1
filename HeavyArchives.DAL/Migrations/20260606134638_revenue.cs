using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavyArchives.DAL.Migrations
{
    /// <inheritdoc />
    public partial class revenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Revenue",
                table: "Tours",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Revenue",
                table: "Concerts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "Concerts");
        }
    }
}
