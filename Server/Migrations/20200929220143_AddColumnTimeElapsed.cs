using Microsoft.EntityFrameworkCore.Migrations;

namespace BomberMan.Server.Migrations
{
    public partial class AddColumnTimeElapsed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TimeElapsed",
                table: "PlayerScores",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeElapsed",
                table: "PlayerScores");
        }
    }
}
