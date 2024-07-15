using Microsoft.AspNetCore.Identity;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;
using TaskManager.Models.DTOs;

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

		#region Methods: Private

		private async Task SignIn(IdentityUserModel user)
		{
			await _signInManager.SignInAsync(user,true);
		}

		private IdentityUserModel CreateUserModel(UserModelDTO loginData)
		{
			var user = new IdentityUserModel()
			{
				UserName = loginData.Username,
				Email = loginData.Email,
				TelegramNickname = loginData.TelegramNickname,
				CreatedDate = DateTime.UtcNow, // TODO: Autofill
				UpdatedDate = DateTime.UtcNow,
			};
			return user;
		}

		#endregion

		#region Methods: Public

		public async Task<ServiceResponse> Login(UserModelDTO loginData, HttpContext httpContext)
		{
			var identityResult = await _signInManager.PasswordSignInAsync(loginData.Username, loginData.Password, true, false);
			var result = new ServiceResponse()
			{
				Success = identityResult.Succeeded,
				Message = identityResult.ToString()
			};
			if (result.Success)
			{
				var user = CreateUserModel(loginData);
				await SignIn(user);
			}
			return result;
		}

		public async Task<ServiceResponse> Register(UserModelDTO loginData, HttpContext httpContext)
		{
			var user = CreateUserModel(loginData);
			var identityResult = await _userManager.CreateAsync(user, loginData.Password);

			var result = new ServiceResponse()
			{
				Success = identityResult.Succeeded,
				Message = identityResult.ToString()
			};
			if (result.Success)
				await SignIn(user);

			return result;
		}

		public async Task<ServiceResponse> Logout(HttpContext httpContext)
		{
			var result = new ServiceResponse();
			try
			{
				await _signInManager.SignOutAsync();
				result.Success = true;
			}
			catch
			{
				result.Success = false;
				result.Message = "Не удалось выйти из учетной записи";
			}
			return result;
		}

		#endregion
	}
}
