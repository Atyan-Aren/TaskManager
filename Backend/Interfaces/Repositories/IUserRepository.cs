namespace TaskManager.Interfaces.Repositories
{
	public interface IUserRepository<TUser> where TUser : class
	{
		Task<IEnumerable<TUser>> GetAllUsers();
		Task<TUser> GetUserByUsername(string username);
		Task<TUser> GetUserByEmail(string email);
		Task<TUser> GetUserByTelegramNickname(string telegramNickname);
		Task<bool> CreateUser(TUser user);
	}
}
