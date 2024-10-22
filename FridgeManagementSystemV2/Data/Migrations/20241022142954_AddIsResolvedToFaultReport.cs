using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeManagementSystemV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsResolvedToFaultReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                table: "FaultReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TechnicianId",
                table: "FaultReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaultReports_TechnicianId",
                table: "FaultReports",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianId",
                table: "FaultReports",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "TechnicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianId",
                table: "FaultReports");

            migrationBuilder.DropIndex(
                name: "IX_FaultReports_TechnicianId",
                table: "FaultReports");

            migrationBuilder.DropColumn(
                name: "IsResolved",
                table: "FaultReports");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "FaultReports");
        }
    }
}
