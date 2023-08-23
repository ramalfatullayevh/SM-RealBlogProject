using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShahnazMammadova.Migrations
{
    /// <inheritdoc />
    public partial class added_blog_ispopular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPopular",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPopular",
                table: "Blogs");
        }
    }
}
