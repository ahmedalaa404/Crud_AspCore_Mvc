using System;
using System.ComponentModel.DataAnnotations;

namespace CrudOperations.Models
{
	public class DepartmentVM
	{

		public int id { get; set; }
		[Required]
		[MaxLength(50, ErrorMessage = "Enter Name Less Than 50 Char")]
		[MinLength(10, ErrorMessage = "Enter Name More Than 10 Char")]
		public string Name { get; set; }
		[Required]
		[MaxLength(20, ErrorMessage = "Enter Name Less Than 20 Char")]
		[MinLength(5, ErrorMessage = "Enter Name More Than 5 Char")]

		public string Code { get; set; }

		public DateTime CreteDate { get; set; } = DateTime.Now;


	}
}
