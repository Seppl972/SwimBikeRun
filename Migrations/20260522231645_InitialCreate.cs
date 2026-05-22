using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimBikeRun.Migrations
{
    public partial class InitialCreate : Migration
    {
        // Up() = SQL um die Tabelle Trainingseinheiten anzulegen
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainingseinheiten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DauerMinuten = table.Column<int>(type: "INTEGER", nullable: false),
                    DistanzKm = table.Column<double>(type: "REAL", nullable: false),
                    Notiz = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingseinheiten", x => x.Id);
                });
        }

        // Down() = SQL um sie wieder zu löschen (Rollback)
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainingseinheiten");
        }
    }
}
