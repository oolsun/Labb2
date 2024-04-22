using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class spelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FK_CourseId",
                table: "Enrollments",
                newName: "FK_SubjectId");

            migrationBuilder.RenameColumn(
                name: "FkTeacherId",
                table: "Class",
                newName: "FK_TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FK_SubjectId",
                table: "Enrollments",
                newName: "FK_CourseId");

            migrationBuilder.RenameColumn(
                name: "FK_TeacherId",
                table: "Class",
                newName: "FkTeacherId");
        }
    }
}
