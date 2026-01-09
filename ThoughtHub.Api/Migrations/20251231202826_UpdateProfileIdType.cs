using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfileIdType : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_FollowMappings_Profiles_FolloweeId",
                table: "FollowMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowMappings_Profiles_FollowerId",
                table: "FollowMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationFollowers_Profiles_ProfileId",
                table: "PublicationFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationMembers_Profiles_ProfileId",
                table: "PublicationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Profiles_OwnerId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingHistories_Profiles_ProfileId",
                table: "ReadingHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Profiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "ReadingHistories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Publications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "PublicationMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "PublicationFollowers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Profiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Profiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "Profiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "FolloweeId",
                table: "FollowMappings",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "FollowerId",
                table: "FollowMappings",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorProfileId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_FollowMappings_Profiles_FolloweeId",
                table: "FollowMappings",
                column: "FolloweeId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowMappings_Profiles_FollowerId",
                table: "FollowMappings",
                column: "FollowerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationFollowers_Profiles_ProfileId",
                table: "PublicationFollowers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationMembers_Profiles_ProfileId",
                table: "PublicationMembers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Profiles_OwnerId",
                table: "Publications",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingHistories_Profiles_ProfileId",
                table: "ReadingHistories",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropForeignKey(
                name: "FK_FollowMappings_Profiles_FolloweeId",
                table: "FollowMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowMappings_Profiles_FollowerId",
                table: "FollowMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationFollowers_Profiles_ProfileId",
                table: "PublicationFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationMembers_Profiles_ProfileId",
                table: "PublicationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Profiles_OwnerId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingHistories_Profiles_ProfileId",
                table: "ReadingHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                table: "Profiles");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "ReadingHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Publications",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "PublicationMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "PublicationFollowers",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "FolloweeId",
                table: "FollowMappings",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "FollowerId",
                table: "FollowMappings",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorProfileId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Profiles_AuthorProfileId",
                table: "Articles",
                column: "AuthorProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Profiles_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowMappings_Profiles_FolloweeId",
                table: "FollowMappings",
                column: "FolloweeId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowMappings_Profiles_FollowerId",
                table: "FollowMappings",
                column: "FollowerId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationFollowers_Profiles_ProfileId",
                table: "PublicationFollowers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationMembers_Profiles_ProfileId",
                table: "PublicationMembers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Profiles_OwnerId",
                table: "Publications",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingHistories_Profiles_ProfileId",
                table: "ReadingHistories",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
