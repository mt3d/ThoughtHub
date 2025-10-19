using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtHub.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurePublicationProfileRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Publications_PublicationId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Publications_PublicationId1",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PublicationId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PublicationId1",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PublicationId1",
                table: "Profiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationId1",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PublicationId",
                table: "Profiles",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PublicationId1",
                table: "Profiles",
                column: "PublicationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Publications_PublicationId",
                table: "Profiles",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Publications_PublicationId1",
                table: "Profiles",
                column: "PublicationId1",
                principalTable: "Publications",
                principalColumn: "PublicationId");
        }
    }
}
