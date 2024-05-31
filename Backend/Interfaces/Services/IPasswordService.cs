namespace TaskManager.Interfaces.Services
{
	public interface IPasswordService
	{
		string GenerateHashedPassword(string password);

		bool PasswordIsEquals(string password, string hashedPassword);
	}
}
