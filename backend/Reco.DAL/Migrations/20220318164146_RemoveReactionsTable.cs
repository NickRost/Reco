using Microsoft.EntityFrameworkCore.Migrations;

namespace Reco.DAL.Migrations
{
    public partial class RemoveReactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoReactions_Users_UserId",
                table: "VideoReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReactions_Videos_VideoId",
                table: "VideoReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions");

            migrationBuilder.RenameTable(
                name: "VideoReactions",
                newName: "VideoReaction");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReactions_VideoId",
                table: "VideoReaction",
                newName: "IX_VideoReaction_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReactions_UserId",
                table: "VideoReaction",
                newName: "IX_VideoReaction_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoReaction",
                table: "VideoReaction",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoReaction_Users_UserId",
                table: "VideoReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReaction_Videos_VideoId",
                table: "VideoReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoReaction",
                table: "VideoReaction");

            migrationBuilder.RenameTable(
                name: "VideoReaction",
                newName: "VideoReactions");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReaction_VideoId",
                table: "VideoReactions",
                newName: "IX_VideoReactions_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoReaction_UserId",
                table: "VideoReactions",
                newName: "IX_VideoReactions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoReactions",
                table: "VideoReactions",
                column: "Id");

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
        }
    }
}
