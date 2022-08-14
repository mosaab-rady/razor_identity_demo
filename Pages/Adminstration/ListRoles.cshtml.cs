using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razorIdentityDemo.Pages;

[Authorize(Roles = "admin")]
public class ListRolesModel : PageModel
{

	private readonly RoleManager<IdentityRole> roleManager;

	public ListRolesModel(RoleManager<IdentityRole> roleManager)
	{
		this.roleManager = roleManager;
	}


	public IEnumerable<IdentityRole> roles { get; set; }
	public void OnGet()
	{
		roles = roleManager.Roles;
	}


}