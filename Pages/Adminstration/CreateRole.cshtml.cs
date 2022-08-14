using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorIdentityDemo.Models;

namespace razorIdentityDemo.Pages;

[Authorize(Roles = "admin")]
public class CreateRoleModel : PageModel
{
	private readonly RoleManager<IdentityRole> roleManager;

	public CreateRoleModel(RoleManager<IdentityRole> roleManager)
	{
		this.roleManager = roleManager;
	}

	public void OnGet()
	{

	}

	[BindProperty]
	public CreateRoleViewModel role { get; set; }

	public async Task<IActionResult> OnPost()
	{
		if (ModelState.IsValid)
		{
			IdentityRole identityRole = new() { Name = role.RoleName };

			var result = await roleManager.CreateAsync(identityRole);

			if (result.Succeeded)
			{
				return RedirectToPage("/adminstration/listroles");
			}

			foreach (var err in result.Errors)
			{
				ModelState.AddModelError("", err.Description);
			}
		}
		return Page();
	}
}