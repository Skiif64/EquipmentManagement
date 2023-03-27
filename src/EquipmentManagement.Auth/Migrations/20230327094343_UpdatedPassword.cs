using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Auth.Migrations
{
    public partial class UpdatedPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9a5c4d1-37c9-44bb-b7ab-af19076ed2e9"));

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordSalt");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new Guid("15ab498b-2a34-41fc-964e-6d047c6f17ec"), "Admin", "GxMmSBwf3vwp0jXcF9OQO6lJXtkDuCbWfteejKDjZhTNTyotEgxswiquvORSYk04", "HxfQhcWMJFaASfzS+h5qvg==", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("15ab498b-2a34-41fc-964e-6d047c6f17ec"));

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "Password");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[] { new Guid("f9a5c4d1-37c9-44bb-b7ab-af19076ed2e9"), "Admin", "example", "Admin" });
        }
    }
}
