using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSS.Migrations
{
    /// <inheritdoc />
    public partial class fixPropsAndRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolygonApps_Farms_FarmId",
                table: "PolygonApps");

            migrationBuilder.DropIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "PolygonApps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FieldId",
                table: "PolygonApps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps",
                column: "FarmId",
                unique: true,
                filter: "[FarmId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PolygonApps_FieldId",
                table: "PolygonApps",
                column: "FieldId",
                unique: true,
                filter: "[FieldId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PolygonApps_Farms_FarmId",
                table: "PolygonApps",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PolygonApps_Fields_FieldId",
                table: "PolygonApps",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolygonApps_Farms_FarmId",
                table: "PolygonApps");

            migrationBuilder.DropForeignKey(
                name: "FK_PolygonApps_Fields_FieldId",
                table: "PolygonApps");

            migrationBuilder.DropIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps");

            migrationBuilder.DropIndex(
                name: "IX_PolygonApps_FieldId",
                table: "PolygonApps");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "PolygonApps");

            migrationBuilder.AlterColumn<int>(
                name: "FarmId",
                table: "PolygonApps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolygonApps_FarmId",
                table: "PolygonApps",
                column: "FarmId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PolygonApps_Farms_FarmId",
                table: "PolygonApps",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
