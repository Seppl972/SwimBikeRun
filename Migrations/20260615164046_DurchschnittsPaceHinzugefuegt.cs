using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimBikeRun.Migrations
{
    /// <inheritdoc />
    public partial class DurchschnittsPaceHinzugefuegt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sportart",
                table: "Trainingseinheiten",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<double>(
                name: "DurchschnittsPace",
                table: "Trainingseinheiten",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurchschnittsPace",
                table: "Trainingseinheiten");

            migrationBuilder.AlterColumn<string>(
                name: "Sportart",
                table: "Trainingseinheiten",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
