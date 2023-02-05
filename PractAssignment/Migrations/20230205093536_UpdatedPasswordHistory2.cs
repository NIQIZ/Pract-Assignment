using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PractAssignment.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPasswordHistory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastPassword1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastPassword2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordHistories_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistories_ApplicationUserId",
                table: "PasswordHistories",
                column: "ApplicationUserId");
        }
    }
}
