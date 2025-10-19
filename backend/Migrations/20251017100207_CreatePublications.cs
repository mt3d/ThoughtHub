using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtHub.Migrations
{
    /// <inheritdoc />
    public partial class CreatePublications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Publication_PublicationId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Profiles");

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

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    PublicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.PublicationId);
                    table.ForeignKey(
                        name: "FK_Publications_Profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicationFollowers",
                columns: table => new
                {
                    PublicationFollowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    FollowedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationFollowers", x => x.PublicationFollowerId);
                    table.ForeignKey(
                        name: "FK_PublicationFollowers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationFollowers_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "PublicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationMembers",
                columns: table => new
                {
                    PublicationMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationMembers", x => x.PublicationMemberId);
                    table.ForeignKey(
                        name: "FK_PublicationMembers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationMembers_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "PublicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PublicationId",
                table: "Profiles",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PublicationId1",
                table: "Profiles",
                column: "PublicationId1");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationFollowers_ProfileId",
                table: "PublicationFollowers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationFollowers_PublicationId",
                table: "PublicationFollowers",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationMembers_ProfileId",
                table: "PublicationMembers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationMembers_PublicationId",
                table: "PublicationMembers",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_OwnerId",
                table: "Publications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_Slug",
                table: "Publications",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Publications_PublicationId",
                table: "Articles",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Publications_PublicationId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Publications_PublicationId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Publications_PublicationId1",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "PublicationFollowers");

            migrationBuilder.DropTable(
                name: "PublicationMembers");

            migrationBuilder.DropTable(
                name: "Publications");

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

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    PublicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.PublicationId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Publication_PublicationId",
                table: "Articles",
                column: "PublicationId",
                principalTable: "Publication",
                principalColumn: "PublicationId");
        }
    }
}
