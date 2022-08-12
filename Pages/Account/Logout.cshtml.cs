using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razorIdentityDemo.Pages;

public class LogoutModel : PageModel
{
	private readonly SignInManager<IdentityUser> signInManager;

	public LogoutModel(SignInManager<IdentityUser> signInManager)
	{
		this.signInManager = signInManager;
	}

	public void OnGet()
	{

	}

	public async Task<IActionResult> OnPostAsync()
	{
		await signInManager.SignOutAsync();
		return RedirectToPage("/Index");
	}
}