using FluentResults;

namespace Domain.Common.Errors;

public class BusinessRuleError : Error
{
    public BusinessRuleError(string message) : base(message)
    {
    }
}
