using TaskManager.Interfaces.Services;
using TaskManager.Models;

namespace TaskManager.Services
{
	public class LoginService : ILoginService
	{
		#region Fields

		#endregion

		#region Constructors

		public LoginService()
		{
		}

		#endregion

		#region Methods: Public

		public async Task<bool> Login(LoginDataModel loginData)
		{
			return await Task.FromResult(true);
		}

		public async Task<bool> Register(LoginDataModel loginData)
		{
			return await Task.FromResult(true);
		}

		#endregion
	}
}
