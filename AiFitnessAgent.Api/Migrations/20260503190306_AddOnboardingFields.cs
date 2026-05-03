using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiFitnessAgent.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOnboardingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "fitness",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaysPerWeek",
                schema: "fitness",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FitnessLevel",
                schema: "fitness",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                schema: "fitness",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Injuries",
                schema: "fitness",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Limitations",
                schema: "fitness",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnboardingCompleted",
                schema: "fitness",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                schema: "fitness",
                table: "Users",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DaysPerWeek",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FitnessLevel",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Goal",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Injuries",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Limitations",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OnboardingCompleted",
                schema: "fitness",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "fitness",
                table: "Users");
        }
    }
}
