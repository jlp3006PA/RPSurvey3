using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPSurvey3.Data.Migrations
{
    public partial class AddCreatedbyCreatedDateEtcToSubSectionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SubSection",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "SubSection",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "SubSection",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "SubSection",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SubSection");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "SubSection");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "SubSection");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "SubSection");
        }
    }
}
