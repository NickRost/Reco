using Microsoft.EntityFrameworkCore.Migrations;

namespace Reco.DAL.Migrations
{
    public partial class FolderIdNullablePropertyAndRemovedParentFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Teams_TeamId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Folders_FolderId",
                table: "Videos");

            migrationBuilder.AlterColumn<int>(
                name: "FolderId",
                table: "Videos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Folders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Teams_TeamId",
                table: "Folders",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Folders_FolderId",
                table: "Videos",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Teams_TeamId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Folders_FolderId",
                table: "Videos");

            migrationBuilder.AlterColumn<int>(
                name: "FolderId",
                table: "Videos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Folders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Teams_TeamId",
                table: "Folders",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Folders_FolderId",
                table: "Videos",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
