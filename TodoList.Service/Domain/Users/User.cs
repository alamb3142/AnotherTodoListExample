using Domain.Common;
using Domain.Common.Errors;
using Domain.Users.Errors;
using FluentResults;

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

	public static Result<User> Create(string username, string hashedPassword, string salt)
	{
		var validUsernameCheck = InvalidUsernameError.Check(username);
		if (validUsernameCheck.IsFailed)
			return validUsernameCheck.ToResult<User>();

		var user = new User(username, hashedPassword, salt);
		return Result.Ok(user);
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
