using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal_CrudOperations.Migrations
{
	public partial class ImageName : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "ImageName",
				table: "employees",
				type: "nvarchar(max)",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ImageName",
				table: "employees");
		}
	}
}
