using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSS.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Fields_FieldId",
                table: "Crops");

            migrationBuilder.DropIndex(
                name: "IX_Crops_FieldId",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Farms");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fields",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FarmId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Fields",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "Farms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Farms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Lat = table.Column<float>(type: "real", nullable: true),
                    Lng = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FarmId",
                table: "Fields",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Farms_FarmId",
                table: "Fields",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Farms_FarmId",
                table: "Fields");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Fields_FarmId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Farms");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Farms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_FieldId",
                table: "Crops",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Fields_FieldId",
                table: "Crops",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
