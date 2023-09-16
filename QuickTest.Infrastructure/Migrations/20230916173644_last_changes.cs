using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTest.Infrastructure.Migrations
{
    public partial class last_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Groups_GroupId",
                table: "GroupTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Teachers_TeacherId",
                table: "GroupTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTeacher",
                table: "GroupTeacher");

            migrationBuilder.RenameTable(
                name: "GroupTeacher",
                newName: "GroupTeachers");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTeacher_TeacherId",
                table: "GroupTeachers",
                newName: "IX_GroupTeachers_TeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTeachers",
                table: "GroupTeachers",
                columns: new[] { "GroupId", "TeacherId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_Groups_GroupId",
                table: "GroupTeachers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_Teachers_TeacherId",
                table: "GroupTeachers",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_Groups_GroupId",
                table: "GroupTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_Teachers_TeacherId",
                table: "GroupTeachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTeachers",
                table: "GroupTeachers");

            migrationBuilder.RenameTable(
                name: "GroupTeachers",
                newName: "GroupTeacher");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTeachers_TeacherId",
                table: "GroupTeacher",
                newName: "IX_GroupTeacher_TeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTeacher",
                table: "GroupTeacher",
                columns: new[] { "GroupId", "TeacherId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Groups_GroupId",
                table: "GroupTeacher",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Teachers_TeacherId",
                table: "GroupTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
