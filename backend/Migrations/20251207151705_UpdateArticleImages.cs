using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticleImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Articles");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleImageId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleImageId",
                table: "Articles",
                column: "ArticleImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Images_ArticleImageId",
                table: "Articles",
                column: "ArticleImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Images_ArticleImageId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleImageId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleImageId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
