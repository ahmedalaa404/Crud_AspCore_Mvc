using System.ComponentModel.DataAnnotations;

namespace CrudOperations.Models
{
	public class ForgetPasswordVM
	{
		[Required]
		[EmailAddress]
        public string Email { get; set; }



    }
}
