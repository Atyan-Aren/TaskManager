using TaskManager.Models;

namespace TaskManager.Interfaces.Services
{
	public interface ILoginService
	{
		Task<bool> Login(LoginDataModel loginData);
		Task<bool> Register(LoginDataModel loginData);
	}
}
