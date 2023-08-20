using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTest.Infrastructure.Migrations
{
    public partial class initialUsersValuesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InitialEmail",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialFirstName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialLastName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialEmail",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialFirstName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialLastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialEmail",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialFirstName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialLastName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialEmail",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "InitialFirstName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "InitialLastName",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "InitialEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InitialFirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InitialLastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InitialEmail",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "InitialFirstName",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "InitialLastName",
                table: "Admins");
        }
    }
}
