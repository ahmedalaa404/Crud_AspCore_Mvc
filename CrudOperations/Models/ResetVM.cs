using System.ComponentModel.DataAnnotations;

namespace CrudOperations.Models
{
	public class ResetVM
	{
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "This Column Is Required")]
		public string NewPassword { get; set; }


		[DataType(DataType.Password)]
		[Required(ErrorMessage = "This Column Is Required")]
		[Compare("NewPassword", ErrorMessage = "Confirm PassWord Not Mutched The Password")]
		public string ConfirmPassword { get; set; }



	}
}

