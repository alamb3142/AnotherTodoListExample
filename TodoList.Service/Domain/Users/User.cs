using Domain.Common;

namespace Domain.Users;

public class User : Entity, IAggregateRoot
{
	public string Username { get; protected set; }
	public string HashedPassword { get; protected set; }
	public string Salt { get; protected set; }

	private User(string username, string hashedPassword, string salt)
	{
		Username = username;
		HashedPassword = hashedPassword;
		Salt = salt;
	}

	public static User Create(string username, string hashedPassword, string salt)
	{
		return new User(username, hashedPassword, salt);
	}

	public void UpdatePassword(string newHashedPassword)
	{
		HashedPassword = newHashedPassword;
	}

#nullable disable
	///<remarks>To keep EF happy, don't use</remarks>
	protected internal User() { }
#nullable enable
}
