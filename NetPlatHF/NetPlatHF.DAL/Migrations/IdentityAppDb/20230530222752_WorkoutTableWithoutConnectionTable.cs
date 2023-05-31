using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPlatHF.DAL.Migrations.IdentityAppDb
{
    /// <inheritdoc />
    public partial class WorkoutTableWithoutConnectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "workouts",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workouts", x => x.Name);
                    table.ForeignKey(
                        name: "FK_workouts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupTemplateWorkout",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    WorkoutName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTemplateWorkout", x => new { x.GroupsId, x.WorkoutName });
                    table.ForeignKey(
                        name: "FK_GroupTemplateWorkout_grouptemplates_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "grouptemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTemplateWorkout_workouts_WorkoutName",
                        column: x => x.WorkoutName,
                        principalTable: "workouts",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTemplateWorkout_WorkoutName",
                table: "GroupTemplateWorkout",
                column: "WorkoutName");

            migrationBuilder.CreateIndex(
                name: "IX_workouts_OwnerId",
                table: "workouts",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTemplateWorkout");

            migrationBuilder.DropTable(
                name: "workouts");
        }
    }
}
