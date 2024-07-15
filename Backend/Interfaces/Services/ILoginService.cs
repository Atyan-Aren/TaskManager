using TaskManager.Models;
using TaskManager.Models.DTOs;

namespace TaskManager.Interfaces.Services
{
    public interface ILoginService
	{
		Task<ServiceResponse> Login(UserModelDTO loginData, HttpContext httpContext);
		Task<ServiceResponse> Register(UserModelDTO loginData, HttpContext httpContext);
		Task<ServiceResponse> Logout(HttpContext httpContext);
	}
}
