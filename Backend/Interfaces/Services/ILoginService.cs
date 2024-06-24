using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
	public interface ILoginService
	{
		Task<ServiceResponse> Login(LoginDataModel loginData, HttpContext httpContext);
		Task<ServiceResponse> Register(LoginDataModel loginData, HttpContext httpContext);

		Task<ServiceResponse> Logout(HttpContext httpContext);
	}
}
