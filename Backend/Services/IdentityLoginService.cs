using Microsoft.AspNetCore.Identity;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Services
{
	public class IdentityLoginService : ILoginService
	{
		#region Fields

		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		#endregion

		#region Constructors

		public IdentityLoginService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#endregion

		#region Methods: Public

		public async Task<bool> Login(LoginDataModel loginData)
		{
			var result = await _signInManager.PasswordSignInAsync(loginData.Username, loginData.Password, true, false);
			return result.Succeeded;
		}

		public async Task<bool> Register(LoginDataModel loginData)
		{
			var user = new IdentityUser() { UserName = loginData.Username, Email = loginData.Email };
			var result = await _userManager.CreateAsync(user, loginData.Password);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);
			}
			return result.Succeeded;
		}

		#endregion
	}
}
