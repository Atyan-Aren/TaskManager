using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;
using TaskManager.Repository.DbContexts;

namespace TaskManager.Services
{
	public class LoginService : ILoginService
	{
		#region Fields

		private ApplicationContext _applicationContext;
		private IPasswordService _passwordService;

		#endregion

		#region Constructors

		public LoginService(ApplicationContext applicationContext, IPasswordService passwordService)
		{
			_applicationContext = applicationContext;
			_passwordService = passwordService;
		}

		#endregion

		#region Methods: Private

		private async Task Authenticate(UserModel user, HttpContext httpContext)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		private async Task Logout(HttpContext httpContext)
		{
			await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//return RedirectToAction("Login", "Account");
		}

		#endregion

		#region Methods: Public

		public async Task<bool> Login(LoginDataModel loginData, HttpContext httpContext)
		{
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == loginData.Email && user.Name == loginData.Username);
			if (user == null)
				return false;
			return _passwordService.PasswordIsEquals(loginData.Password, user.Password);
		}

		public async Task<bool> Register(LoginDataModel loginData, HttpContext httpContext)
		{
			var userModel = new UserModel()
			{
				Name = loginData.Username,
				Email = loginData.Email,
				Password = _passwordService.GenerateHashedPassword(loginData.Password),
				TelegramNickname = loginData.TelegramNickname,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow,
			};
			_applicationContext.Users.Add(userModel);
			var insertedCount = await _applicationContext.SaveChangesAsync();
			await Authenticate(userModel, httpContext);
			return insertedCount > 0;
		}

		#endregion
	}
}