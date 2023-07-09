using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRepairGarage.Data.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_GaragesServices_GarageId_ServiceId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_GarageId_ServiceId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_GarageId",
                table: "Appointments",
                column: "GarageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_GarageId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_GarageId_ServiceId",
                table: "Appointments",
                columns: new[] { "GarageId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_GaragesServices_GarageId_ServiceId",
                table: "Appointments",
                columns: new[] { "GarageId", "ServiceId" },
                principalTable: "GaragesServices",
                principalColumns: new[] { "ServiceId", "GarageId" });
        }
    }
}
