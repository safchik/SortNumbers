using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SortNumbers.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SortResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAscending = table.Column<bool>(type: "bit", nullable: false),
                    TimeTaken = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SortedNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    SortResultId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortedNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SortedNumbers_SortResults_SortResultId",
                        column: x => x.SortResultId,
                        principalTable: "SortResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SortedNumbers_SortResultId",
                table: "SortedNumbers",
                column: "SortResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SortedNumbers");

            migrationBuilder.DropTable(
                name: "SortResults");
        }
    }
}
