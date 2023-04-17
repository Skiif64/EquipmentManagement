using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Auth.Migrations
{
    public partial class AddedIsBlockedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9450d985-3675-42b3-bac8-3460dcda0fb5"));

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsBlocked", "Login", "PasswordHash", "PasswordSalt", "RefreshToken", "Role" },
                values: new object[] { new Guid("84384909-b6c9-44e6-b095-797270115457"), false, "Admin", "tJXRQpp66y9PQD+KtjmgTRBtmV/sl/O9yszklMkSyJXLRraN1vEdpWvd8zsSCvpC", "dBzooEGMaYAbkGqd9UziOg==", null, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84384909-b6c9-44e6-b095-797270115457"));

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "PasswordSalt", "RefreshToken", "Role" },
                values: new object[] { new Guid("9450d985-3675-42b3-bac8-3460dcda0fb5"), "Admin", "IN+zoaqsaFPhRb1+w3Pp4ZPwOkiHCpE+uPJAj+sNASUB3HoyiQI9w0unhb3S9sbP", "3mgmsCuUc/QH0LJvNypVAA==", null, "Admin" });
        }
    }
}
