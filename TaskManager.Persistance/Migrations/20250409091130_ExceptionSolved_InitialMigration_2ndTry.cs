using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ExceptionSolved_InitialMigration_2ndTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkTasks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "WorkTaskPriorityTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastModificationDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "WorkTaskStatusTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastModificationDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "WorkTasks",
                columns: new[] { "Id", "AssignedEmployeeId", "CreationDate", "Description", "EndDate", "LastModificationDate", "Name", "PriorityId", "StartDate", "StatusId" },
                values: new object[] { 1, "", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "WorkTaskPriorityTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastModificationDate" },
                values: new object[] { new DateTime(2025, 4, 9, 10, 59, 57, 814, DateTimeKind.Local).AddTicks(1604), new DateTime(2025, 4, 9, 10, 59, 57, 815, DateTimeKind.Local).AddTicks(7407) });

            migrationBuilder.UpdateData(
                table: "WorkTaskStatusTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastModificationDate" },
                values: new object[] { new DateTime(2025, 4, 9, 10, 59, 57, 819, DateTimeKind.Local).AddTicks(3806), new DateTime(2025, 4, 9, 10, 59, 57, 819, DateTimeKind.Local).AddTicks(3818) });
        }
    }
}
