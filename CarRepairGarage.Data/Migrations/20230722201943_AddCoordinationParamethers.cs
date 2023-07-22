using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRepairGarage.Data.Migrations
{
    public partial class AddCoordinationParamethers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Primary key");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Addresses",
                type: "float",
                nullable: true,
                comment: "Address Latitude coordinate");

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Addresses",
                type: "float",
                nullable: true,
                comment: "Address Longitude coordinate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Primary key");
        }
    }
}
