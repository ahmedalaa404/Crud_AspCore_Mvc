using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dal_CrudOperations.DomainModel
{
	public class Department
	{
		public int id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]

		public string Code { get; set; }

		public DateTime CreteDate { get; set; } = DateTime.Now;



		public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
	}


}
