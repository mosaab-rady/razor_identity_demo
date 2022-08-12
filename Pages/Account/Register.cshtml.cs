using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorIdentityDemo.Models;

namespace razorIdentityDemo.Pages;

public class AccountModel : PageModel
{
	private readonly UserManager<IdentityUser> userManager;
	private readonly SignInManager<IdentityUser> signInManager;

	public AccountModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
	{
		this.userManager = userManager;
		this.signInManager = signInManager;
	}

	[BindProperty]
	public RegisterViewModel model { get; set; }


	public PageResult OnGet()
	{
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var user = new IdentityUser { Email = model.Email, UserName = model.Name };

		var result = await userManager.CreateAsync(user, model.Password);

		if (result.Succeeded)
		{
			await signInManager.SignInAsync(user, isPersistent: false);
			return RedirectToPage("/Index");
		}

		foreach (var err in result.Errors)
		{
			ModelState.AddModelError("", err.Description);
		}

		return Page();

	}

}