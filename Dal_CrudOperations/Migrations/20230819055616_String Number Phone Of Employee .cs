using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal_CrudOperations.Migrations
{
	public partial class StringNumberPhoneOfEmployee : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "NumberPhone",
				table: "employees",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "NumberPhone",
				table: "employees",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);
		}
	}
}
