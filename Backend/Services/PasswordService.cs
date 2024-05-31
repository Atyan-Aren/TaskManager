using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TaskManager.Interfaces.Services;

namespace TaskManager.Services
{
	public class PasswordService : IPasswordService
	{
		#region Fields

		public static readonly byte[] _salt = new byte[] { 1, 2, 3, 4, 5 };

		#endregion

		#region Methods: Public

		public string GenerateHashedPassword(string password)
		{                                                    
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: _salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));
			return hashed;
		}

		public bool PasswordIsEquals(string password, string hashedPassword)
		{
			var newHashedPassword = GenerateHashedPassword(password);
			return newHashedPassword == hashedPassword;
		}

		#endregion
	}
}
