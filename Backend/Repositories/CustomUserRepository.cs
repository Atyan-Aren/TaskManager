using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models.DBModels;
using TaskManager.Repositories.DbContexts;

namespace TaskManager.Repositories
{
	public class CustomUserRepository : IUserRepository<UserModel>
	{
		#region Fields

		private ApplicationContext _applicationContext;

		#endregion

		#region Constructors

		public CustomUserRepository(ApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		#endregion

		#region Methods: Public

		public async Task<bool> CreateUser(UserModel user)
		{
			_applicationContext.Users.Add(user);
			var insertedCount = await _applicationContext.SaveChangesAsync();
			return insertedCount > 0;
		}

		public async Task<IEnumerable<UserModel>> GetAllUsers()
		{
			var users = await _applicationContext.Users.ToListAsync();
			return users;
		}

		public async Task<UserModel> GetUserByEmail(string email)
		{
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email);
			return user;
		}

		public async Task<UserModel> GetUserByTelegramNickname(string telegramNickname)
		{
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.TelegramNickname == telegramNickname);
			return user;
		}

		public async Task<UserModel> GetUserByUsername(string username)
		{
			var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Name == username);
			return user;
		}

		#endregion
	}
}
