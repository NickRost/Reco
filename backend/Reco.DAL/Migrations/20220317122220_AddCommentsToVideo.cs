using Microsoft.EntityFrameworkCore.Migrations;

namespace Reco.DAL.Migrations
{
    public partial class AddCommentsToVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReaction_Comments_CommentId",
                table: "CommentReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentReaction_Users_UserId",
                table: "CommentReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReaction_Users_UserId",
                table: "VideoReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReaction_Videos_VideoId",
                table: "VideoReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoReaction",
                table: "VideoReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReaction",
                table: "CommentReaction");

            migrationBuilder.RenameTable(
                name: "VideoReaction",
                newName: "VideoReactions");

            migrationBuilder.RenameTable(
                name: "CommentReaction",
                newName: "CommentReactions");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReaction_VideoId",
                table: "VideoReactions",
                newName: "IX_VideoReactions_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReaction_UserId",
                table: "VideoReactions",
                newName: "IX_VideoReactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReaction_UserId",
                table: "CommentReactions",
                newName: "IX_CommentReactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReaction_CommentId",
                table: "CommentReactions",
                newName: "IX_CommentReactions_CommentId");

            migrationBuilder.AddColumn<int>(
                name: "FolderId1",
                table: "Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoId1",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_FolderId1",
                table: "Videos",
                column: "FolderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId1",
                table: "Comments",
                column: "VideoId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoId1",
                table: "Comments",
                column: "VideoId1",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoReactions_Users_UserId",
                table: "VideoReactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoReactions_Videos_VideoId",
                table: "VideoReactions",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Folders_FolderId1",
                table: "Videos",
                column: "FolderId1",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_Comments_CommentId",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_Users_UserId",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReactions_Users_UserId",
                table: "VideoReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReactions_Videos_VideoId",
                table: "VideoReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Folders_FolderId1",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_FolderId1",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Comments_VideoId1",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions");

            migrationBuilder.DropColumn(
                name: "FolderId1",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoId1",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "VideoReactions",
                newName: "VideoReaction");

            migrationBuilder.RenameTable(
                name: "CommentReactions",
                newName: "CommentReaction");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReactions_VideoId",
                table: "VideoReaction",
                newName: "IX_VideoReaction_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReactions_UserId",
                table: "VideoReaction",
                newName: "IX_VideoReaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReactions_UserId",
                table: "CommentReaction",
                newName: "IX_CommentReaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReactions_CommentId",
                table: "CommentReaction",
                newName: "IX_CommentReaction_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoReaction",
                table: "VideoReaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReaction",
                table: "CommentReaction",
                column: "Id");

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
                name: "FK_VideoReaction_Users_UserId",
                table: "VideoReaction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoReaction_Videos_VideoId",
                table: "VideoReaction",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
