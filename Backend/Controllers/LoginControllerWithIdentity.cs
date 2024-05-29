using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
	//TODO: ПЕРЕДЕЛАТЬ В ОБЩИЙ, сделать два хелпера для identity custom
	[Route("Login")]
	public class LoginControllerWithIdentity : Controller
	{
		#region Fields

		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		#endregion

		#region Constructors

		public LoginControllerWithIdentity(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#endregion

		#region Methods: Public

		[Route("Login/{username}")]
		public string Login(string username)
		{
			return "HELLO" + username;
		}

		[Route("Register")]
		public async Task<string> Register(string username, string password)
		{
			var user = new IdentityUser()
			{
				UserName = username,
			};
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);
			}
			return "HELLO" + username + password;
		}

		#endregion
	}
}
