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

		private async void SignIn(UserModel user, HttpContext httpContext)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		#endregion

		#region Methods: Public

		public async Task<ServiceResult> Login(LoginDataModel loginData, HttpContext httpContext)
		{
			var result = new ServiceResult();
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == loginData.Email && user.Name == loginData.Username);
			if (user == null)
			{
				result.Success = false;
				result.Message = "Такого пользователя не существует";
			}
			else if (!_passwordService.PasswordIsEquals(loginData.Password, user.Password))
			{
				result.Success = false;
				result.Message = "Неверный пароль";
			}
			else
			{
				result.Success = true;
				SignIn(user, httpContext);
			}
			return result;
		}

		public async Task<ServiceResult> Register(LoginDataModel loginData, HttpContext httpContext)
		{
			var result = new ServiceResult();
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == loginData.Email && user.Name == loginData.Username);
			if (user != null)
			{
				result.Success = false;
				result.Message = "Такой пользователь уже существует";
			}
			else
			{
				var userModel = new UserModel()
				{
					Name = loginData.Username,
					Email = loginData.Email,
					Password = _passwordService.GenerateHashedPassword(loginData.Password),
					TelegramNickname = loginData.TelegramNickname,
					CreatedDate = DateTime.UtcNow, // TODO: Autofill
					UpdatedDate = DateTime.UtcNow,
				};
				_applicationContext.Users.Add(userModel);
				var insertedCount = await _applicationContext.SaveChangesAsync();
				if (insertedCount <= 0)
				{
					result.Success = false;
					result.Message = "Не удалось зарегестрировать пользователья";
				}
				else
				{
					result.Success = true;
					SignIn(userModel, httpContext);
				}
			}
			return result;
		}

		public async Task<ServiceResult> Logout(HttpContext httpContext)
		{
			var result = new ServiceResult();
			try
			{
				await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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