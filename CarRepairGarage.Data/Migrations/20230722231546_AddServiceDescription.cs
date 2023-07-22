using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRepairGarage.Data.Migrations
{
    public partial class AddServiceDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "Service Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Services");
        }
    }
}
