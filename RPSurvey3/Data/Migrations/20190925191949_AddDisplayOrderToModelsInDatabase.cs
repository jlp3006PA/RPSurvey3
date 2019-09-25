using Microsoft.EntityFrameworkCore.Migrations;

namespace RPSurvey3.Data.Migrations
{
    public partial class AddDisplayOrderToModelsInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "SubSection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Section",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Question",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "SubSection");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Question");
        }
    }
}
