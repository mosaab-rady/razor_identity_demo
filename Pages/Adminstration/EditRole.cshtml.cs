using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorIdentityDemo.Models;

namespace razorIdentityDemo.Pages;

public class EditRoleModel : PageModel
{

	private readonly RoleManager<IdentityRole> roleManager;
	private readonly UserManager<IdentityUser> userManager;

	public EditRoleModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
	{
		this.roleManager = roleManager;
		this.userManager = userManager;
	}

	[BindProperty]
	public EditRoleViewModel model { get; set; }

	public async Task<IActionResult> OnGet(string id)
	{
		var role = await roleManager.FindByIdAsync(id);

		if (role is null)
		{
			return NotFound();
		}

		model = new EditRoleViewModel
		{
			Id = role.Id,
			RoleName = role.Name,

		};


		foreach (IdentityUser user in userManager.Users.ToList())
		{
			if (await userManager.IsInRoleAsync(user, role.Name))
			{
				model.Users.Add(user.Email);
			}
		}

		return Page();
	}



	public async Task<IActionResult> OnPost(string id)
	{
		var role = await roleManager.FindByIdAsync(id);

		if (role is null)
		{
			return NotFound();
		}


		role.Name = model.RoleName;
		var result = await roleManager.UpdateAsync(role);

		if (result.Succeeded)
		{
			return RedirectToPage("/adminstration/listroles");
		}


		foreach (var err in result.Errors)
		{
			ModelState.AddModelError("", err.Description);
		}

		return Page();
	}
}