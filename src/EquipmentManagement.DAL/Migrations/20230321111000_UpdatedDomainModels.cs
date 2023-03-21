using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.DAL.Migrations
{
    public partial class UpdatedDomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Equipments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StatusId",
                table: "EquipmentRecords",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EmployeeId",
                table: "Equipments",
                column: "EmployeeId");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Employees_EmployeeId",
                table: "Equipments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
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

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Employees_EmployeeId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_EmployeeId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRecords_EmployeeId",
                table: "EquipmentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRecords_EquipmentId",
                table: "EquipmentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRecords_StatusId",
                table: "EquipmentRecords");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Equipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "StatusId",
                table: "EquipmentRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
