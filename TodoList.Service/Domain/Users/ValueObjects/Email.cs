using Domain.Common;

namespace Domain.Users;

public class Email : ValueObject
{
	public string Value { get; }
	public bool Verified { get; private set; }

	private Email(string value, bool verified)
	{
		Value = value;
		Verified = verified;
	}

	public static Email Create(string value)
	{
		return new Email(value, false);
	}

	public void Verify()
	{
		Verified = true;
	}

    protected override IEnumerable<object> GetEqualityComponents()
    {
    	yield return Value;
    	yield return Verified;
    }
}

