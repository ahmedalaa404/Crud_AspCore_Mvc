using System.ComponentModel.DataAnnotations;

namespace CrudOperations.Models
{
	public class RegisterVM
	{


		public string Fname { get; set; }
		public string Lname { get; set; }
		[Required(ErrorMessage = "This Column Is Required")]
		[EmailAddress(ErrorMessage = "Enter Email Valid ")]

		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "This Column Is Required")]
		public string Password { get; set; }


		[DataType(DataType.Password)]
		[Required(ErrorMessage = "This Column Is Required")]
		[Compare("Password", ErrorMessage = "Confirm PassWord Not Mutched The Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
		[RegularExpression("^(?:\\+20|0)?1[0-9]{9}$")]
		public string PhoneNumber { get; set; }



	}
}
