using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSS.Migrations
{
    /// <inheritdoc />
    public partial class AddPropForLocationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_CropTypes_CropTypeId",
                table: "Crops");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Farms_FarmId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_FarmId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Crops_CropTypeId",
                table: "Crops");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Locations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Locations",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FarmId",
                table: "Fields",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_CropTypeId",
                table: "Crops",
                column: "CropTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_CropTypes_CropTypeId",
                table: "Crops",
                column: "CropTypeId",
                principalTable: "CropTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Farms_FarmId",
                table: "Fields",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
