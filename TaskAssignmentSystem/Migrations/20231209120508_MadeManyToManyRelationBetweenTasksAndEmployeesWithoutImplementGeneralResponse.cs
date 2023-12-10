using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAssignmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class MadeManyToManyRelationBetweenTasksAndEmployeesWithoutImplementGeneralResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_BodyEmployeeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_BodyTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_BodyTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BodyEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BodyTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ErrCode",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ErrMsg",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "BodyEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ErrCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ErrMsg",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BodyTaskId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrCode",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrMsg",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BodyEmployeeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrCode",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrMsg",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BodyTaskId",
                table: "Tasks",
                column: "BodyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BodyEmployeeId",
                table: "Employees",
                column: "BodyEmployeeId");

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
    }
}
