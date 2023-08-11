using System.ComponentModel.DataAnnotations.Schema;

namespace Dal_CrudOperations.DomainModel
{
	[NotMapped]
	public class Email
	{
		public int id { get; set; }
		public string Subject { get; set; }

		public string Body { get; set; }
		public string To { get; set; }




	}
}
