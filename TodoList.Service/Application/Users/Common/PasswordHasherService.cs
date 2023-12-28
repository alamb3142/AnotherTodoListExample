using System.Text;
using Domain.Users;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Application.Users.Common;

public interface IPasswordHasherService
{
	public string CreateSalt();
	public string HashPassword(string password, string salt);
	public bool VerifyPassword(string password, User user);
}

public class PasswordHasherService : IPasswordHasherService
{
	public string CreateSalt()
	{
		return Guid.NewGuid().ToString();
	}

	public string HashPassword(string password, string salt)
	{
		return Convert.ToBase64String(
			KeyDerivation.Pbkdf2(
				password: password!,
				salt: Encoding.UTF8.GetBytes(salt),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8
			)
		);
	}

	public bool VerifyPassword(string password, User user)
	{
		return HashPassword(password, user.Salt) == user.HashedPassword;
	}
}
