using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPlatHF.DAL.Migrations.IdentityAppDb
{
    /// <inheritdoc />
    public partial class TemplatesConnectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplates_AspNetUsers_OwnerId",
                table: "ExerciseTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupExerciseTemplate_ExerciseTemplates_ExerciseId",
                table: "GroupExerciseTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupExerciseTemplate_GroupTemplates_GroupId",
                table: "GroupExerciseTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTemplates_AspNetUsers_OwnerId",
                table: "GroupTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTemplates",
                table: "GroupTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseTemplates",
                table: "ExerciseTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupExerciseTemplate",
                table: "GroupExerciseTemplate");

            migrationBuilder.RenameTable(
                name: "GroupTemplates",
                newName: "grouptemplates");

            migrationBuilder.RenameTable(
                name: "ExerciseTemplates",
                newName: "exercisetemplates");

            migrationBuilder.RenameTable(
                name: "GroupExerciseTemplate",
                newName: "templates");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTemplates_OwnerId",
                table: "grouptemplates",
                newName: "IX_grouptemplates_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTemplates_OwnerId",
                table: "exercisetemplates",
                newName: "IX_exercisetemplates_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupExerciseTemplate_GroupId",
                table: "templates",
                newName: "IX_templates_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupExerciseTemplate_ExerciseId",
                table: "templates",
                newName: "IX_templates_ExerciseId");

            migrationBuilder.AddColumn<int>(
                name: "Repetitions",
                table: "templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_grouptemplates",
                table: "grouptemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_exercisetemplates",
                table: "exercisetemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_templates",
                table: "templates",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 12, 10 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 10, 5 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 8, 15 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 10, 20 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 16, 30 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 6, 12 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 14, 40 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Repetitions", "Weight" },
                values: new object[] { 0, 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_exercisetemplates_AspNetUsers_OwnerId",
                table: "exercisetemplates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_grouptemplates_AspNetUsers_OwnerId",
                table: "grouptemplates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_templates_exercisetemplates_ExerciseId",
                table: "templates",
                column: "ExerciseId",
                principalTable: "exercisetemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_templates_grouptemplates_GroupId",
                table: "templates",
                column: "GroupId",
                principalTable: "grouptemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exercisetemplates_AspNetUsers_OwnerId",
                table: "exercisetemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_grouptemplates_AspNetUsers_OwnerId",
                table: "grouptemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_templates_exercisetemplates_ExerciseId",
                table: "templates");

            migrationBuilder.DropForeignKey(
                name: "FK_templates_grouptemplates_GroupId",
                table: "templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grouptemplates",
                table: "grouptemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_exercisetemplates",
                table: "exercisetemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_templates",
                table: "templates");

            migrationBuilder.DropColumn(
                name: "Repetitions",
                table: "templates");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "templates");

            migrationBuilder.RenameTable(
                name: "grouptemplates",
                newName: "GroupTemplates");

            migrationBuilder.RenameTable(
                name: "exercisetemplates",
                newName: "ExerciseTemplates");

            migrationBuilder.RenameTable(
                name: "templates",
                newName: "GroupExerciseTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_grouptemplates_OwnerId",
                table: "GroupTemplates",
                newName: "IX_GroupTemplates_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_exercisetemplates_OwnerId",
                table: "ExerciseTemplates",
                newName: "IX_ExerciseTemplates_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_templates_GroupId",
                table: "GroupExerciseTemplate",
                newName: "IX_GroupExerciseTemplate_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_templates_ExerciseId",
                table: "GroupExerciseTemplate",
                newName: "IX_GroupExerciseTemplate_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTemplates",
                table: "GroupTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseTemplates",
                table: "ExerciseTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupExerciseTemplate",
                table: "GroupExerciseTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplates_AspNetUsers_OwnerId",
                table: "ExerciseTemplates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupExerciseTemplate_ExerciseTemplates_ExerciseId",
                table: "GroupExerciseTemplate",
                column: "ExerciseId",
                principalTable: "ExerciseTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupExerciseTemplate_GroupTemplates_GroupId",
                table: "GroupExerciseTemplate",
                column: "GroupId",
                principalTable: "GroupTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTemplates_AspNetUsers_OwnerId",
                table: "GroupTemplates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
