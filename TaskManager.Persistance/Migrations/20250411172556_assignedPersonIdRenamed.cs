using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class assignedPersonIdRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedEmployeeId",
                table: "WorkTasks",
                newName: "AssignedPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedPersonId",
                table: "WorkTasks",
                newName: "AssignedEmployeeId");
        }
    }
}
