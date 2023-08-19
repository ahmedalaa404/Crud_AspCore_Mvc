using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal_CrudOperations.DomainModel
{
	public class Employee
	{
		public int id { get; set; }
		[Required]
		public string name { get; set; }
		[Required]
		public int Salary { get; set; }

		[Range(22, 50)]
		public int age { get; set; }

		public String EmailAddress { get; set; }

		public bool IsActive { get; set; }


		public string NumberPhone { get; set; }

		public DateTime HireDate { get; set; }

		public DateTime CreteDate { get; set; } = DateTime.Now;
		[ForeignKey("department")]
		public int? Departmentid { get; set; }
		public Department department { get; set; }

		public string ImageName { get; set; }
	}
}
