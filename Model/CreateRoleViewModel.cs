using System.ComponentModel.DataAnnotations;

namespace razorIdentityDemo.Models;

public class CreateRoleViewModel
{
	[Required]
	public string RoleName { get; set; }
}