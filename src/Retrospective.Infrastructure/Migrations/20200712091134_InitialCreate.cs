using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Retrospective.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Retrospective");

            migrationBuilder.CreateTable(
                name: "Sprints",
                schema: "Retrospective",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SprintItems",
                schema: "Retrospective",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SprintItemType = table.Column<int>(type: "int", nullable: false),
                    SprintId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SprintItems_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalSchema: "Retrospective",
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "Retrospective",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    SprintItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Items_SprintItems_SprintItemId",
                        column: x => x.SprintItemId,
                        principalSchema: "Retrospective",
                        principalTable: "SprintItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SprintItemId",
                schema: "Retrospective",
                table: "Items",
                column: "SprintItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintItems_SprintId",
                schema: "Retrospective",
                table: "SprintItems",
                column: "SprintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items",
                schema: "Retrospective");

            migrationBuilder.DropTable(
                name: "SprintItems",
                schema: "Retrospective");

            migrationBuilder.DropTable(
                name: "Sprints",
                schema: "Retrospective");
        }
    }
}
