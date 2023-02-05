using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PractAssignment.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPasswordHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PasswordHistory_PasswordHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PasswordHistory");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PasswordHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHistoryId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PasswordHistoryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastPassword1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastPassword2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PasswordHistoryId",
                table: "AspNetUsers",
                column: "PasswordHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PasswordHistory_PasswordHistoryId",
                table: "AspNetUsers",
                column: "PasswordHistoryId",
                principalTable: "PasswordHistory",
                principalColumn: "Id");
        }
    }
}
