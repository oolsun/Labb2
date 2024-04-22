using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class smallchangesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Subjects_SubjectId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_SubjectId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_FK_SubjectId",
                table: "Enrollments",
                column: "FK_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Subjects_FK_SubjectId",
                table: "Enrollments",
                column: "FK_SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Subjects_FK_SubjectId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_FK_SubjectId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SubjectId",
                table: "Enrollments",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Subjects_SubjectId",
                table: "Enrollments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");
        }
    }
}
