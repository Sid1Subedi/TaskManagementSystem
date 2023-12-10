using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAssignmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialTaskAssignDbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrMsg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_employees_employees_BodyEmployeeId",
                        column: x => x.BodyEmployeeId,
                        principalTable: "employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ErrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrMsg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_tasks_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tasks_tasks_BodyTaskId",
                        column: x => x.BodyTaskId,
                        principalTable: "tasks",
                        principalColumn: "TaskId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_BodyEmployeeId",
                table: "employees",
                column: "BodyEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_BodyTaskId",
                table: "tasks",
                column: "BodyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_EmployeeId",
                table: "tasks",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
