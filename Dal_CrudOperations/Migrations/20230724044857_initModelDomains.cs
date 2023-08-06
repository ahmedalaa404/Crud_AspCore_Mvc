using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Dal_CrudOperations.Migrations
{
	public partial class initModelDomains : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Departments",
				columns: table => new
				{
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
					CreteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Departments", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "employees",
				columns: table => new
				{
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					Salary = table.Column<int>(type: "int", nullable: false),
					age = table.Column<int>(type: "int", nullable: false),
					EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
					IsActive = table.Column<bool>(type: "bit", nullable: false),
					NumberPhone = table.Column<int>(type: "int", nullable: false),
					HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_employees", x => x.id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Departments");

			migrationBuilder.DropTable(
				name: "employees");
		}
	}
}
