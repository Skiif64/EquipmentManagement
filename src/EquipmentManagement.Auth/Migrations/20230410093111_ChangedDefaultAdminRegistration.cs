using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Auth.Migrations
{
    public partial class ChangedDefaultAdminRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84384909-b6c9-44e6-b095-797270115457"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsBlocked", "Login", "PasswordHash", "PasswordSalt", "RefreshToken", "Role" },
                values: new object[] { new Guid("84384909-b6c9-44e6-b095-797270115457"), false, "Admin", "tJXRQpp66y9PQD+KtjmgTRBtmV/sl/O9yszklMkSyJXLRraN1vEdpWvd8zsSCvpC", "dBzooEGMaYAbkGqd9UziOg==", null, "Admin" });
        }
    }
}
