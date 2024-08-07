﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TaskManager.Exceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;
using TaskManager.Models.DTOs;

namespace TaskManager.Services
{
    public class LoginService : ILoginService
	{
		#region Fields

		private IUserRepository<UserModel> _userRepository;
		private IPasswordService _passwordService;

		#endregion

		#region Constructors

		public LoginService(IUserRepository<UserModel> userRepository, IPasswordService passwordService)
		{
			_userRepository = userRepository;
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
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.TelegramNickname),
			};
			ClaimsIdentity claimsId = new ClaimsIdentity(claims, "TaskManagerCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsId));
		}

		private async Task CheckUserIsNotExists(string username)
		{
			var user = await _userRepository.GetUserByUsername(username);
			if (user != null)
			{
				throw new LoginException("Такой пользователь уже существует");
			}
		}

		private void CheckUserIsExists(UserModel user)
		{
			if (user == null)
			{
				throw new LoginException("Такого пользователя не существует");
			}
		}

		private void CheckPassword(string password, string userPassword)
		{
			if (!_passwordService.PasswordIsEquals(password, userPassword))
			{
				throw new LoginException("Неверный пароль");
			}

		}

		#endregion

		#region Methods: Public

		public async Task<ServiceResponse> Login(UserModelDTO loginData, HttpContext httpContext)
		{
			var user = await _userRepository.GetUserByUsername(loginData.Username);
			CheckUserIsExists(user);
			CheckPassword(loginData.Password, user.Password);

			SignIn(user, httpContext);

			return new ServiceResponse() { Success = true };
		}

		public async Task<ServiceResponse> Register(UserModelDTO loginData, HttpContext httpContext)
		{
			await CheckUserIsNotExists(loginData.Username);

			var userModel = loginData.Map();
			await _userRepository.CreateUser(userModel);

			SignIn(userModel, httpContext);

            return new ServiceResponse() { Success = true };
        }

		public async Task<ServiceResponse> Logout(HttpContext httpContext)
		{
			await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new ServiceResponse() { Success = true };
        }

		#endregion
	}
}