using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAssignmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeToOnModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Employees_EmployeeId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Employees_EmployeeId",
                table: "TaskAssignments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Employees_EmployeeId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Employees_EmployeeId",
                table: "TaskAssignments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }
    }
}
