using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShahnazMammadova.Migrations
{
    /// <inheritdoc />
    public partial class updated_storycomments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryCommentId",
                table: "BlogComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoryComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryComments_BlogComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "BlogComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoryComments_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_StoryCommentId",
                table: "BlogComments",
                column: "StoryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryComments_ParentCommentId",
                table: "StoryComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryComments_StoryId",
                table: "StoryComments",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryComments_UserId",
                table: "StoryComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_StoryComments_StoryCommentId",
                table: "BlogComments",
                column: "StoryCommentId",
                principalTable: "StoryComments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_StoryComments_StoryCommentId",
                table: "BlogComments");

            migrationBuilder.DropTable(
                name: "StoryComments");

            migrationBuilder.DropIndex(
                name: "IX_BlogComments_StoryCommentId",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "StoryCommentId",
                table: "BlogComments");
        }
    }
}
