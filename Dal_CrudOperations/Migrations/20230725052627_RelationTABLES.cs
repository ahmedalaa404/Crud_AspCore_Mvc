using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal_CrudOperations.Migrations
{
	public partial class RelationTABLES : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "employees",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AddColumn<int>(
				name: "Departmentid",
				table: "employees",
				type: "int",
				nullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "Departments",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Code",
				table: "Departments",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_employees_Departmentid",
				table: "employees",
				column: "Departmentid");

			migrationBuilder.AddForeignKey(
				name: "FK_employees_Departments_Departmentid",
				table: "employees",
				column: "Departmentid",
				principalTable: "Departments",
				principalColumn: "id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_employees_Departments_Departmentid",
				table: "employees");

			migrationBuilder.DropIndex(
				name: "IX_employees_Departmentid",
				table: "employees");

			migrationBuilder.DropColumn(
				name: "Departmentid",
				table: "employees");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "employees",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "Departments",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.AlterColumn<string>(
				name: "Code",
				table: "Departments",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");
		}
	}
}
