using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Reco.DAL.Migrations
{
    public partial class AddVideoSharing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_Comments_CommentId",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_Users_UserId",
                table: "CommentReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions");

            migrationBuilder.RenameTable(
                name: "CommentReactions",
                newName: "CommentReaction");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReactions_UserId",
                table: "CommentReaction",
                newName: "IX_CommentReaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReactions_CommentId",
                table: "CommentReaction",
                newName: "IX_CommentReaction_CommentId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Videos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Folders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "VideoId1",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReaction",
                table: "CommentReaction",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccessesForRegisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VideoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessesForRegisteredUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessesForRegisteredUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessesForRegisteredUsers_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessesForUnregisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    VideoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessesForUnregisteredUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessesForUnregisteredUsers_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId1",
                table: "Comments",
                column: "VideoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccessesForRegisteredUsers_UserId",
                table: "AccessesForRegisteredUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessesForRegisteredUsers_VideoId",
                table: "AccessesForRegisteredUsers",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessesForUnregisteredUsers_VideoId",
                table: "AccessesForUnregisteredUsers",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReaction_Comments_CommentId",
                table: "CommentReaction",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReaction_Users_UserId",
                table: "CommentReaction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoId1",
                table: "Comments",
                column: "VideoId1",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReaction_Comments_CommentId",
                table: "CommentReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentReaction_Users_UserId",
                table: "CommentReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoId1",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "AccessesForRegisteredUsers");

            migrationBuilder.DropTable(
                name: "AccessesForUnregisteredUsers");

            migrationBuilder.DropIndex(
                name: "IX_Comments_VideoId1",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReaction",
                table: "CommentReaction");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "VideoId1",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "CommentReaction",
                newName: "CommentReactions");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReaction_UserId",
                table: "CommentReactions",
                newName: "IX_CommentReactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReaction_CommentId",
                table: "CommentReactions",
                newName: "IX_CommentReactions_CommentId");

            migrationBuilder.AddColumn<List<string>>(
                name: "SharedEmails",
                table: "Videos",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Folders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_Comments_CommentId",
                table: "CommentReactions",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_Users_UserId",
                table: "CommentReactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
