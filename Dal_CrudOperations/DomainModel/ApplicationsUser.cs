using Microsoft.AspNetCore.Identity;

namespace Dal_CrudOperations.DomainModel
{
	public class ApplicationsUser : IdentityUser
	{
		public string Fname { get; set; }
		public string Lname { get; set; }

		public bool IsAgree { get; set; }

		public string PhoneNumber { get; set; }

	}
}
