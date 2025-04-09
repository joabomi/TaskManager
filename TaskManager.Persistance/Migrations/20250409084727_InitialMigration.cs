using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkTaskPriorityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PriorityWeight = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTaskPriorityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTaskStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTaskStatusTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkTasks_WorkTaskPriorityTypes_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "WorkTaskPriorityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkTasks_WorkTaskStatusTypes_StatusId",
                        column: x => x.StatusId,
                        principalTable: "WorkTaskStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkTaskPriorityTypes",
                columns: new[] { "Id", "CreationDate", "LastModificationDate", "Name", "PriorityWeight" },
                values: new object[] { 1, new DateTime(2025, 4, 9, 10, 47, 26, 714, DateTimeKind.Local).AddTicks(658), new DateTime(2025, 4, 9, 10, 47, 26, 726, DateTimeKind.Local).AddTicks(708), "Name", 0 });

            migrationBuilder.InsertData(
                table: "WorkTaskStatusTypes",
                columns: new[] { "Id", "CreationDate", "LastModificationDate", "Name" },
                values: new object[] { 1, new DateTime(2025, 4, 9, 10, 47, 26, 730, DateTimeKind.Local).AddTicks(2659), new DateTime(2025, 4, 9, 10, 47, 26, 730, DateTimeKind.Local).AddTicks(2678), "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkTaskPriorityTypes_PriorityWeight",
                table: "WorkTaskPriorityTypes",
                column: "PriorityWeight",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_PriorityId",
                table: "WorkTasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_StatusId",
                table: "WorkTasks",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkTasks");

            migrationBuilder.DropTable(
                name: "WorkTaskPriorityTypes");

            migrationBuilder.DropTable(
                name: "WorkTaskStatusTypes");
        }
    }
}
