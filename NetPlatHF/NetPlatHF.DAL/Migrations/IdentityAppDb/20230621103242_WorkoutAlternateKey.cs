using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPlatHF.DAL.Migrations.IdentityAppDb
{
    /// <inheritdoc />
    public partial class WorkoutAlternateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTemplateWorkout_workouts_WorkoutName",
                table: "GroupTemplateWorkout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workouts",
                table: "workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTemplateWorkout",
                table: "GroupTemplateWorkout");

            migrationBuilder.DropIndex(
                name: "IX_GroupTemplateWorkout_WorkoutName",
                table: "GroupTemplateWorkout");

            migrationBuilder.DropColumn(
                name: "WorkoutName",
                table: "GroupTemplateWorkout");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "workouts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "GroupTemplateWorkout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_workouts_Name",
                table: "workouts",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workouts",
                table: "workouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTemplateWorkout",
                table: "GroupTemplateWorkout",
                columns: new[] { "GroupsId", "WorkoutId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTemplateWorkout_WorkoutId",
                table: "GroupTemplateWorkout",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTemplateWorkout_workouts_WorkoutId",
                table: "GroupTemplateWorkout",
                column: "WorkoutId",
                principalTable: "workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTemplateWorkout_workouts_WorkoutId",
                table: "GroupTemplateWorkout");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_workouts_Name",
                table: "workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workouts",
                table: "workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTemplateWorkout",
                table: "GroupTemplateWorkout");

            migrationBuilder.DropIndex(
                name: "IX_GroupTemplateWorkout_WorkoutId",
                table: "GroupTemplateWorkout");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "workouts");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "GroupTemplateWorkout");

            migrationBuilder.AddColumn<string>(
                name: "WorkoutName",
                table: "GroupTemplateWorkout",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workouts",
                table: "workouts",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTemplateWorkout",
                table: "GroupTemplateWorkout",
                columns: new[] { "GroupsId", "WorkoutName" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTemplateWorkout_WorkoutName",
                table: "GroupTemplateWorkout",
                column: "WorkoutName");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTemplateWorkout_workouts_WorkoutName",
                table: "GroupTemplateWorkout",
                column: "WorkoutName",
                principalTable: "workouts",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
