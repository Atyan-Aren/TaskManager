using Microsoft.EntityFrameworkCore;
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

		#region Methods: Public

		public async Task<bool> Login(LoginDataModel loginData)
		{
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == loginData.Email && user.Name == loginData.Username);
			if (user == null)
				return false;
			return _passwordService.PasswordIsEquals(loginData.Password, user.Password);
		}

		public async Task<bool> Register(LoginDataModel loginData)
		{
			var userModel = new UserModel()
			{
				Name = loginData.Username,
				Email = loginData.Email,
				Password = _passwordService.GenerateHashedPassword(loginData.Password),
				TelegramNickname = loginData.TelegramNickname,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow,
			};
			_applicationContext.Users.Add(userModel);
			var insertedCount = await _applicationContext.SaveChangesAsync();
			return insertedCount > 0;
		}

		#endregion
	}
}
