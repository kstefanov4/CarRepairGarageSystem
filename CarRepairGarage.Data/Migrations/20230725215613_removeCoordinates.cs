using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRepairGarage.Data.Migrations
{
    public partial class removeCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
