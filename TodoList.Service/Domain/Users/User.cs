using Domain.Common;

namespace Domain.Users;

public class User : Entity, IAggregateRoot
{
	public Email Email { get; protected set; }

	private User(Email email)
	{
		Email = email;
	}

	public static User Create(string email)
	{
		return new User(Email.Create(email));
	}

#nullable disable
	///<remarks>To keep EF happy, don't use</remarks>
	protected internal User() {}
#nullable enable
}
