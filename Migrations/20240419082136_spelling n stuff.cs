using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class spellingnstuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FK_SubjectId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_FK_SubjectId",
                table: "Teachers",
                column: "FK_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Subjects_FK_SubjectId",
                table: "Teachers",
                column: "FK_SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Subjects_FK_SubjectId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_FK_SubjectId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FK_SubjectId",
                table: "Teachers");
        }
    }
}
