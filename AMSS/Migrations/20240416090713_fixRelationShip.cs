using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSS.Migrations
{
    /// <inheritdoc />
    public partial class fixRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps");

            migrationBuilder.CreateIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps",
                column: "FarmId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps");

            migrationBuilder.CreateIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps",
                column: "FarmId");
        }
    }
}
