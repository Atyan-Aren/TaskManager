using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Interfaces.Services;
using TaskManager.Models;

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
		public async Task<bool> Login([FromBody]LoginDataModel loginData)
		{
			return await _loginService.Login(loginData, HttpContext);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<bool> Register([FromBody]LoginDataModel loginData)
		{
			return await _loginService.Register(loginData, HttpContext);
		}

		#endregion
	}
}
