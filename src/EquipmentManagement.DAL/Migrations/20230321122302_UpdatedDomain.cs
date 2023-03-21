using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.DAL.Migrations
{
    public partial class UpdatedDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRecords_EmployeeId",
                table: "EquipmentRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRecords_EquipmentId",
                table: "EquipmentRecords",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRecords_StatusId",
                table: "EquipmentRecords",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRecords_Employees_EmployeeId",
                table: "EquipmentRecords",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRecords_Equipments_EquipmentId",
                table: "EquipmentRecords",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRecords_Statuses_StatusId",
                table: "EquipmentRecords",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRecords_Employees_EmployeeId",
                table: "EquipmentRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRecords_Equipments_EquipmentId",
                table: "EquipmentRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRecords_Statuses_StatusId",
                table: "EquipmentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRecords_EmployeeId",
                table: "EquipmentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRecords_EquipmentId",
                table: "EquipmentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRecords_StatusId",
                table: "EquipmentRecords");
        }
    }
}
