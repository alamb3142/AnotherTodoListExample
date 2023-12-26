using Domain.Users;
using FluentAssertions;

namespace Testing.Domain.Users;

[TestFixture]
public class UserTests
{
	[Test]
	public void Create_WithEmail_CreatesUser()
	{
		string email = "email@email.com";
		var user = User.Create(email);
		user.Should().NotBeNull();
		user.Email.Value.Should().Be(email);
	}

	[Test]
	public void Create_WithInvalidEmail_CreatesUser()
	{
		string email = "notAnEmail";
		var user = User.Create(email);
		user.Should().NotBeNull("Because email validation is an application layer concern");
		user.Email.Value.Should().Be(email);
	}
}
