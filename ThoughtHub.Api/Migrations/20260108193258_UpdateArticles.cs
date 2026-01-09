using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Profiles_AuthorProfileId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Profiles_AuthorId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorProfileId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Profiles_AuthorProfileId",
                table: "Articles",
                column: "AuthorProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Profiles_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Profiles_AuthorProfileId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Profiles_AuthorId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorProfileId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Profiles_AuthorProfileId",
                table: "Articles",
                column: "AuthorProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Profiles_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
