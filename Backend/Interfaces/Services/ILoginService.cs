using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
	public interface ILoginService
	{
		Task<bool> Login(LoginDataModel loginData, HttpContext httpContext);
		Task<bool> Register(LoginDataModel loginData, HttpContext httpContext);
	}
}
