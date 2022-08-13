using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorIdentityDemo.Models;

namespace razorIdentityDemo.Pages;

public class EditUserInRoleModel : PageModel
{
	private readonly RoleManager<IdentityRole> roleManager;

	private readonly UserManager<IdentityUser> userManager;
	public EditUserInRoleModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
	{
		model = new List<EditUserInRoleViewModel>();
		this.roleManager = roleManager;
		this.userManager = userManager;
	}


	[BindProperty]
	public List<EditUserInRoleViewModel> model { get; set; }




	public async Task<IActionResult> OnGet(string roleId)
	{
		// ViewBag.roleId=roleId;
		ViewData["roleId"] = roleId;

		var role = await roleManager.FindByIdAsync(roleId);

		if (role is null)
		{
			return NotFound();
		}


		foreach (var user in userManager.Users.ToList())
		{
			var userRoleViewModel = new EditUserInRoleViewModel
			{
				UserId = user.Id,
				Email = user.Email,
			};


			if (await userManager.IsInRoleAsync(user, role.Id))
			{
				userRoleViewModel.IsSelected = true;
			}
			else
			{
				userRoleViewModel.IsSelected = false;
			}

			model.Add(userRoleViewModel);
		}

		return Page();
	}





	public async Task<IActionResult> OnPost(string roleId)
	{
		var role = await roleManager.FindByIdAsync(roleId);

		if (role is null)
		{
			return NotFound();
		}

		for (int i = 0; i < model.Count; i++)
		{
			var user = await userManager.FindByIdAsync(model[i].UserId);

			IdentityResult result = null;

			if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
			{
				result = await userManager.AddToRoleAsync(user, role.Name);
			}
			else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
			{
				result = await userManager.RemoveFromRoleAsync(user, role.Name);
			}
			else
			{
				continue;
			}

			if (result.Succeeded)
			{
				if (i < model.Count - 1)
				{
					continue;
				}
				else
				{
					return RedirectToPage("/adminstration/editrole", new { id = roleId });
				}
			}
		}
		return RedirectToPage("/adminstration/editrole", new { id = roleId });
	}
}