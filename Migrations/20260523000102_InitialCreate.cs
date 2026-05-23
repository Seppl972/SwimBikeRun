using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimBikeRun.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainingseinheiten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sportart = table.Column<string>(type: "TEXT", nullable: false),
                    DauerMinuten = table.Column<int>(type: "INTEGER", nullable: false),
                    DistanzKm = table.Column<double>(type: "REAL", nullable: false),
                    Notiz = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingseinheiten", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainingseinheiten");
        }
    }
}
