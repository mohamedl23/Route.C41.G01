using System.ComponentModel.DataAnnotations;

namespace Route.C41.G01.PL.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="First Name Is Required")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "User Name Is Required")]
		public string Username { get; set; }
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage ="Password Is Required")]
		[MinLength(5 ,ErrorMessage ="Minimum Password Length is 5") ]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password Is Required")]
		[Compare(nameof(Password),ErrorMessage ="Confirm Password doesn't match with password")]
		public string ConfirmPassword { get; set; }
		public bool ISAgree { get; set; }
	}
}
