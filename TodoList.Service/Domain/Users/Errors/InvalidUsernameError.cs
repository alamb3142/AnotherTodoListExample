using Domain.Common.Errors;
using FluentResults;

namespace Domain.Users.Errors;

public class InvalidUsernameError : BusinessRuleError
{
    public InvalidUsernameError()
        : base("Username must be between 5 and 20 characters") { }

    public static Result Check(string username)
    {
    	var length = username.Trim().Length;
    	if (length < 5 || length > 20)
    		return Result.Fail(new InvalidUsernameError());

    	return Result.Ok();
    }
}
