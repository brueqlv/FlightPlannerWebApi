﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Airports_AirportCode",
                table: "Airports");

            migrationBuilder.AlterColumn<string>(
                name: "AirportCode",
                table: "Airports",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AirportCode",
                table: "Airports",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AirportCode",
                table: "Airports",
                column: "AirportCode",
                unique: true);
        }
    }
}
