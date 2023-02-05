using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PractAssignment.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPasswordHistory3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastChanged",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastPassword1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastPassword2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChanged",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastPassword1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastPassword2",
                table: "AspNetUsers");
        }
    }
}
