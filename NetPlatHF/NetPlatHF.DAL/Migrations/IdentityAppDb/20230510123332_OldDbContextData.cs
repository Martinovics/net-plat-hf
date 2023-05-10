using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NetPlatHF.DAL.Migrations.IdentityAppDb
{
    /// <inheritdoc />
    public partial class OldDbContextData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExerciseTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Muscle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupExerciseTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupExerciseTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupExerciseTemplate_ExerciseTemplates_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupExerciseTemplate_GroupTemplates_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExerciseTemplates",
                columns: new[] { "Id", "Description", "Muscle", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "", "Back", "Pull up", 0 },
                    { 2, "", "Back", "Pull down", 0 },
                    { 3, "", "Back", "Row", 0 },
                    { 4, "", "Chest", "Bench press", 0 },
                    { 5, "", "Chest", "Chest fly", 0 },
                    { 6, "Incline", "Chest", "Bench press", 0 },
                    { 7, "Incline", "Biceps", "Curl", 0 },
                    { 8, "Concentration", "Biceps", "Curl", 0 },
                    { 9, "Reverse ez", "Biceps", "Curl", 0 },
                    { 10, "", "Triceps", "JM press", 0 },
                    { 11, "Cable", "Triceps", "Overhead extension", 0 },
                    { 12, "Cable", "Triceps", "Push down", 0 },
                    { 13, "", "Shoulders", "Overhead press", 0 },
                    { 14, "", "Shoulders", "Lateral raise", 0 },
                    { 15, "", "Shoulders", "Face pull", 0 },
                    { 16, "Lower abs", "Abs", "Leg raise", 0 },
                    { 17, "", "Abs", "Ab crunch", 0 },
                    { 18, "Cable", "Abs", "Woodchopper", 0 },
                    { 19, "", "Legs", "Squat", 0 },
                    { 20, "", "Legs", "Leg extension", 0 },
                    { 21, "", "Legs", "Calf raise", 0 }
                });

            migrationBuilder.InsertData(
                table: "GroupTemplates",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "", "Back and chest", 0 },
                    { 2, "", "Arms", 0 },
                    { 3, "", "Legs and abs", 0 }
                });

            migrationBuilder.InsertData(
                table: "GroupExerciseTemplate",
                columns: new[] { "Id", "ExerciseId", "GroupId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 4, 1 },
                    { 4, 6, 1 },
                    { 5, 7, 2 },
                    { 6, 10, 2 },
                    { 7, 13, 2 },
                    { 8, 16, 3 },
                    { 9, 17, 3 },
                    { 10, 19, 3 },
                    { 11, 20, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupExerciseTemplate_ExerciseId",
                table: "GroupExerciseTemplate",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupExerciseTemplate_GroupId",
                table: "GroupExerciseTemplate",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupExerciseTemplate");

            migrationBuilder.DropTable(
                name: "ExerciseTemplates");

            migrationBuilder.DropTable(
                name: "GroupTemplates");

            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "AspNetUsers");
        }
    }
}
