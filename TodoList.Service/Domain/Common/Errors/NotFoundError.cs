using FluentResults;

namespace Domain.Common.Errors;

public class NotFoundError : Error
{
    public NotFoundError(int id, string entityName)
        : base($"Couldn't find {entityName} with ID {id}") { }
}
