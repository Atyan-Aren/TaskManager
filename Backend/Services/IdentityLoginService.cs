using Microsoft.AspNetCore.Identity;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;

namespace TaskManager.Services
{
	public class IdentityLoginService : ILoginService
	{
		#region Fields

		private readonly UserManager<IdentityUserModel> _userManager;
		private readonly SignInManager<IdentityUserModel> _signInManager;

		#endregion

		#region Constructors

		public IdentityLoginService(UserManager<IdentityUserModel> userManager, SignInManager<IdentityUserModel> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#endregion

		#region Methods: Public

		public async Task<bool> Login(LoginDataModel loginData, HttpContext httpContext)
		{
			var result = await _signInManager.PasswordSignInAsync(loginData.Username, loginData.Password, true, false);
			return result.Succeeded;
		}

		public async Task<bool> Register(LoginDataModel loginData, HttpContext httpContext)
		{
			var user = new IdentityUserModel() 
			{
				UserName = loginData.Username,
				Email = loginData.Email,
				TelegramNickname = loginData.TelegramNickname,
			};
			var result = await _userManager.CreateAsync(user, loginData.Password);

			//TODO: Non unterstandable
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);
			}

			return result.Succeeded;
		}

		#endregion
	}
}
