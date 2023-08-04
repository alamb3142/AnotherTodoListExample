using FluentResults;

namespace Domain.Common.ValueObjects;

public class Title : ValueObject
{
    private const int MAX_LENGTH = 50;
    public string Value { get; }

    private Title(string value)
    {
        Value = value;
    }

    public static Result<Title> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value.Trim()))
            return Result.Fail("Title cannot be empty");

        if (value.Length > MAX_LENGTH)
            return Result.Fail($"Title cannot be longer than {MAX_LENGTH} characters");

        return Result.Ok(new Title(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
