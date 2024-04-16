using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSS.Migrations
{
    /// <inheritdoc />
    public partial class AddPointAndPolygonAppTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PolygonAppId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PolygonAppId",
                table: "Farms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PolygonApps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolygonApps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    lng = table.Column<float>(type: "real", nullable: false),
                    PolygonAppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_PolygonApps_PolygonAppId",
                        column: x => x.PolygonAppId,
                        principalTable: "PolygonApps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Farms_PolygonAppId",
                table: "Farms",
                column: "PolygonAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PolygonAppId",
                table: "Positions",
                column: "PolygonAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_PolygonApps_PolygonAppId",
                table: "Farms",
                column: "PolygonAppId",
                principalTable: "PolygonApps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farms_PolygonApps_PolygonAppId",
                table: "Farms");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "PolygonApps");

            migrationBuilder.DropIndex(
                name: "IX_Farms_PolygonAppId",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "PolygonAppId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "PolygonAppId",
                table: "Farms");
        }
    }
}
