using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiFitnessAgent.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailIndexToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                schema: "fitness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DayIndex = table.Column<int>(type: "integer", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    CompletedExerciseIdsJson = table.Column<string>(type: "text", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutSessions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "fitness",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutSessions_WorkoutPlans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "fitness",
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "fitness",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_PlanId",
                schema: "fitness",
                table: "WorkoutSessions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_UserId",
                schema: "fitness",
                table: "WorkoutSessions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutSessions",
                schema: "fitness");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "fitness",
                table: "Users");
        }
    }
}
