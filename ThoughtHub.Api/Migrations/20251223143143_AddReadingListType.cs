using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtHub.Migrations
{
    /// <inheritdoc />
    public partial class AddReadingListType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReadingLists_OwnerId_Slug",
                table: "ReadingLists");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ReadingLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingLists_OwnerId_Slug",
                table: "ReadingLists",
                columns: new[] { "OwnerId", "Slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingLists_OwnerId_Type",
                table: "ReadingLists",
                columns: new[] { "OwnerId", "Type" },
                unique: true,
                filter: "[Type] = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListArticles_ReadingLists_ReadingListId",
                table: "ReadingListArticles",
                column: "ReadingListId",
                principalTable: "ReadingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListArticles_ReadingLists_ReadingListId",
                table: "ReadingListArticles");

            migrationBuilder.DropIndex(
                name: "IX_ReadingLists_OwnerId_Slug",
                table: "ReadingLists");

            migrationBuilder.DropIndex(
                name: "IX_ReadingLists_OwnerId_Type",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ReadingLists");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingLists_OwnerId_Slug",
                table: "ReadingLists",
                columns: new[] { "OwnerId", "Slug" });
        }
    }
}
