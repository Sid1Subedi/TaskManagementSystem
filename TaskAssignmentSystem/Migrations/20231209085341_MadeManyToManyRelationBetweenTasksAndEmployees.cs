using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAssignmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class MadeManyToManyRelationBetweenTasksAndEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_employees_BodyEmployeeId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_employees_EmployeeId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_tasks_BodyTaskId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_EmployeeId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "tasks");

            migrationBuilder.RenameTable(
                name: "tasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_BodyTaskId",
                table: "Tasks",
                newName: "IX_Tasks_BodyTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_BodyEmployeeId",
                table: "Employees",
                newName: "IX_Employees_BodyEmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => new { x.TaskId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_EmployeeId",
                table: "TaskAssignments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_BodyEmployeeId",
                table: "Employees",
                column: "BodyEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_BodyTaskId",
                table: "Tasks",
                column: "BodyTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_BodyEmployeeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_BodyTaskId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "tasks");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employees");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_BodyTaskId",
                table: "tasks",
                newName: "IX_tasks_BodyTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BodyEmployeeId",
                table: "employees",
                newName: "IX_employees_BodyEmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks",
                table: "tasks",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_EmployeeId",
                table: "tasks",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_employees_BodyEmployeeId",
                table: "employees",
                column: "BodyEmployeeId",
                principalTable: "employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_employees_EmployeeId",
                table: "tasks",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_tasks_BodyTaskId",
                table: "tasks",
                column: "BodyTaskId",
                principalTable: "tasks",
                principalColumn: "TaskId");
        }
    }
}
