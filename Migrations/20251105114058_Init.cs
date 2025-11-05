using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWellBeingApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCategoryMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessName = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategoryMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProcessName = table.Column<string>(type: "TEXT", nullable: false),
                    UsageDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DurationSeconds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDebts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceCategory = table.Column<string>(type: "TEXT", nullable: false),
                    TargetCategory = table.Column<string>(type: "TEXT", nullable: false),
                    Ratio = table.Column<double>(type: "REAL", nullable: false),
                    TriggerHours = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDebts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryMappings_ProcessName",
                table: "AppCategoryMappings",
                column: "ProcessName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDebts_SourceCategory_TargetCategory",
                table: "CategoryDebts",
                columns: new[] { "SourceCategory", "TargetCategory" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCategoryMappings");

            migrationBuilder.DropTable(
                name: "AppUsages");

            migrationBuilder.DropTable(
                name: "CategoryDebts");
        }
    }
}
