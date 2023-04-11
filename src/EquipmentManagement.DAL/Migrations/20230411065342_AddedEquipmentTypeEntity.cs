using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.DAL.Migrations
{
    public partial class AddedEquipmentTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipments");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Equipments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_TypeId",
                table: "Equipments",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_EquipmentTypes_TypeId",
                table: "Equipments",
                column: "TypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_EquipmentTypes_TypeId",
                table: "Equipments");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_TypeId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Equipments");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Equipments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
