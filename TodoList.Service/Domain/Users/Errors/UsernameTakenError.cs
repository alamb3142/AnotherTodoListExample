
using Domain.Common.Errors;

namespace Domain.Users.Errors;

public class UsernameTakenError : BusinessRuleError
{
    public UsernameTakenError()
        : base("That name is already taken") { }
}
