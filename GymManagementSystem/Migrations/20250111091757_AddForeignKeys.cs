using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassEnrollments_GymClasses_GymClassId",
                table: "ClassEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_ClassEnrollments_GymClassId",
                table: "ClassEnrollments");

            migrationBuilder.DropColumn(
                name: "GymClassId",
                table: "ClassEnrollments");

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollments_ClassId",
                table: "ClassEnrollments",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassEnrollments_GymClasses_ClassId",
                table: "ClassEnrollments",
                column: "ClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassEnrollments_GymClasses_ClassId",
                table: "ClassEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_ClassEnrollments_ClassId",
                table: "ClassEnrollments");

            migrationBuilder.AddColumn<int>(
                name: "GymClassId",
                table: "ClassEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollments_GymClassId",
                table: "ClassEnrollments",
                column: "GymClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassEnrollments_GymClasses_GymClassId",
                table: "ClassEnrollments",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
