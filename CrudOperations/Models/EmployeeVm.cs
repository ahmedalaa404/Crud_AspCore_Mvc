using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
namespace CrudOperations.Models
{

	public class EmployeeVm
	{

		public int id { get; set; }
		[Required]
		[MaxLength(50, ErrorMessage = "Enter Name Less Than 50 Char")]
		[MinLength(10, ErrorMessage = "Enter Name More Than 10 Char")]

		public string name { get; set; }
		[Required(ErrorMessage = "This Inpute Is Requeried")]

		public int Salary { get; set; }

		[Range(22, 50, ErrorMessage = "Must Enter Number Between 22 To 50")]
		public int age { get; set; }
		[EmailAddress]
		public String EmailAddress { get; set; }

		public bool IsActive { get; set; }


		[Required]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$")]


		public int NumberPhone { get; set; }

		public DateTime HireDate { get; set; }

		public DateTime CreteDate { get; set; } = DateTime.Now;
		public IFormFile Image { get; set; }

		public int? departmentid { get; set; }

		public Department department { get; set; }

		public string ImageName { get; set; }

	}
}
