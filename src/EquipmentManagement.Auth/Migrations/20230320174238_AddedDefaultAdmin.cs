using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Auth.Migrations
{
    public partial class AddedDefaultAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[] { new Guid("6578b55d-1621-4a6a-8e77-7ed8f325003a"), "Admin", "example", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6578b55d-1621-4a6a-8e77-7ed8f325003a"));
        }
    }
}
