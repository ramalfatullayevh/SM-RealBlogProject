using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShahnazMammadova.Migrations
{
    /// <inheritdoc />
    public partial class updated_usermail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserMails");

            migrationBuilder.DropColumn(
                name: "MailMessage",
                table: "UserMails");

            migrationBuilder.DropColumn(
                name: "MailSubject",
                table: "UserMails");

            migrationBuilder.DropColumn(
                name: "ReadDate",
                table: "UserMails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserMails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MailMessage",
                table: "UserMails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MailSubject",
                table: "UserMails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadDate",
                table: "UserMails",
                type: "datetime2",
                nullable: true);
        }
    }
}
