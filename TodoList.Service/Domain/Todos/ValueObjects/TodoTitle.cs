using Domain.Common;
using FluentResults;

namespace Domain.Todos.ValueObjects;

public class TodoTitle : ValueObject
{
    public string Value { get; }

    private TodoTitle(string value)
    {
        Value = value;
    }

    public static Result<TodoTitle> Create(string value)
    {
        return new TodoTitle(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
