using Microsoft.EntityFrameworkCore.Migrations;

namespace Reco.DAL.Migrations
{
    public partial class AddIsSavingFlagToVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSaving",
                table: "Videos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSaving",
                table: "Videos");
        }
    }
}
