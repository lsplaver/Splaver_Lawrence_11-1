using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuarterlySales.Migrations
{
    public partial class AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Employees",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "EmployeeName",
                value: "Ada Lovelace");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "DateOfHire", "EmployeeName", "FirstName", "LastName", "ManagerId" },
                values: new object[] { 2, new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "John", "Doe", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Employees");
        }
    }
}
