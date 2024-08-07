﻿using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DTOs;

namespace TaskManager.Controllers
{
    [Route("Login")]
	public class LoginController : Controller
	{
		#region Fields

		private readonly ILoginService _loginService;

		#endregion

		#region Constructors

		public LoginController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		#endregion

		#region Methods: Public

		[HttpPost]
		[Route("Login")]
		public async Task<ServiceResponse> Login([FromBody]UserModelDTO loginData)
		{
			return await _loginService.Login(loginData, HttpContext);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<ServiceResponse> Register([FromBody]UserModelDTO loginData)
		{
			return await _loginService.Register(loginData, HttpContext);
		}

		[HttpPost]
		[Route("Logout")]
		public async Task<ServiceResponse> Logout()
		{
			return await _loginService.Logout(HttpContext);
		}

		#endregion
	}
}
