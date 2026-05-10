using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiFitnessAgent.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardioMinutes",
                schema: "fitness",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FocusAreas",
                schema: "fitness",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkoutSplit",
                schema: "fitness",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardioMinutes",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FocusAreas",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkoutSplit",
                schema: "fitness",
                table: "Users");
        }
    }
}
