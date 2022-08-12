using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace razorIdentityDemo.Models;

public class RegisterViewModel
{
	[Required]
	public string Name { get; set; }
	[Required]
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	[DataType(DataType.Password)]
	[DisplayName("Confirm Password")]
	[Compare("Password", ErrorMessage = "Password and Password confirm must be the same.")]
	public string ConfirmPassword { get; set; }
}