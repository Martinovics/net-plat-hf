using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPlatHF.DAL.Migrations.IdentityAppDb
{
    /// <inheritdoc />
    public partial class TemplateOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "GroupTemplates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ExerciseTemplates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 11,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 12,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 13,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 14,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 15,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 16,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 17,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 18,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 19,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 20,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 21,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "GroupTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "GroupTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "GroupTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_GroupTemplates_OwnerId",
                table: "GroupTemplates",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplates_OwnerId",
                table: "ExerciseTemplates",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplates_AspNetUsers_OwnerId",
                table: "ExerciseTemplates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTemplates_AspNetUsers_OwnerId",
                table: "GroupTemplates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplates_AspNetUsers_OwnerId",
                table: "ExerciseTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTemplates_AspNetUsers_OwnerId",
                table: "GroupTemplates");

            migrationBuilder.DropIndex(
                name: "IX_GroupTemplates_OwnerId",
                table: "GroupTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseTemplates_OwnerId",
                table: "ExerciseTemplates");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "GroupTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "ExerciseTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 5,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 6,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 7,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 8,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 9,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 10,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 11,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 12,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 13,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 14,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 15,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 16,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 17,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 18,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 19,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 20,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 21,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GroupTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GroupTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "GroupTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 0);
        }
    }
}
