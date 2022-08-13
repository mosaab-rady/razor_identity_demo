using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorIdentityDemo.Models;

namespace razorIdentityDemo.Pages;

public class LoginModel : PageModel
{

	[BindProperty]
	public LoginViewMoedel login { get; set; }
	private readonly UserManager<IdentityUser> userManager;
	private readonly SignInManager<IdentityUser> signInManager;

	public LoginModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager = null)
	{
		this.userManager = userManager;
		this.signInManager = signInManager;
	}


	public void OnGet()
	{

	}




	public async Task<IActionResult> OnPostAsync([FromQuery] string ReturnUrl)
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}
		var user = await userManager.FindByEmailAsync(login.Email);

		var result = await signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);

		if (result.Succeeded)
		{
			if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
			{
				return Redirect(ReturnUrl);
			}
			else
			{
				return RedirectToPage("/Index");
			}
		}

		ModelState.AddModelError("", "Invalid Email or Password");

		return Page();

	}
}