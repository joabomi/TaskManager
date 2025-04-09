using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ExceptionSolved_InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WorkTaskPriorityTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastModificationDate" },
                values: new object[] { new DateTime(2025, 4, 9, 10, 47, 26, 714, DateTimeKind.Local).AddTicks(658), new DateTime(2025, 4, 9, 10, 47, 26, 726, DateTimeKind.Local).AddTicks(708) });

            migrationBuilder.UpdateData(
                table: "WorkTaskStatusTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastModificationDate" },
                values: new object[] { new DateTime(2025, 4, 9, 10, 47, 26, 730, DateTimeKind.Local).AddTicks(2659), new DateTime(2025, 4, 9, 10, 47, 26, 730, DateTimeKind.Local).AddTicks(2678) });
        }
    }
}
