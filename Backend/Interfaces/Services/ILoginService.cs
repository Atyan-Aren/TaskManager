using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
	public interface ILoginService
	{
		Task<ServiceResult> Login(LoginDataModel loginData, HttpContext httpContext);
		Task<ServiceResult> Register(LoginDataModel loginData, HttpContext httpContext);

		Task<ServiceResult> Logout(HttpContext httpContext);
	}
}
