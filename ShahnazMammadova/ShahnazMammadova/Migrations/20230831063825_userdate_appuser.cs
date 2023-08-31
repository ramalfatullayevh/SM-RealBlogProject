using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShahnazMammadova.Migrations
{
    /// <inheritdoc />
    public partial class userdate_appuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDate",
                table: "AspNetUsers");
        }
    }
}
