using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.DAL.Migrations
{
    public partial class ChangedUserIdToUsernameInJournalRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_ApplicationUser_ApplicationUserId",
                table: "Journal");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_Journal_ApplicationUserId",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Journal");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Journal",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Journal");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Journal",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<Guid>(type: "uuid", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journal_ApplicationUserId",
                table: "Journal",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_ApplicationUser_ApplicationUserId",
                table: "Journal",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
