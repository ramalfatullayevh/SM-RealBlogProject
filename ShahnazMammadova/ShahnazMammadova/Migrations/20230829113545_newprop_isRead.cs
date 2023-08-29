using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShahnazMammadova.Migrations
{
    /// <inheritdoc />
    public partial class newprop_isRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Mails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Mails");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Contacts");
        }
    }
}
