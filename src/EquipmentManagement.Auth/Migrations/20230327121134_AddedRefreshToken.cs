using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Auth.Migrations
{
    public partial class AddedRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("15ab498b-2a34-41fc-964e-6d047c6f17ec"));

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshToken",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "PasswordSalt", "RefreshToken", "Role" },
                values: new object[] { new Guid("9450d985-3675-42b3-bac8-3460dcda0fb5"), "Admin", "IN+zoaqsaFPhRb1+w3Pp4ZPwOkiHCpE+uPJAj+sNASUB3HoyiQI9w0unhb3S9sbP", "3mgmsCuUc/QH0LJvNypVAA==", null, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9450d985-3675-42b3-bac8-3460dcda0fb5"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new Guid("15ab498b-2a34-41fc-964e-6d047c6f17ec"), "Admin", "GxMmSBwf3vwp0jXcF9OQO6lJXtkDuCbWfteejKDjZhTNTyotEgxswiquvORSYk04", "HxfQhcWMJFaASfzS+h5qvg==", "Admin" });
        }
    }
}
