using Application.Todos.CompleteTodo;
using Domain.Common.Errors;
using Domain.Todos;
using FluentResults;

namespace Application.UnitTests.Todos.CompleteTodo;

[TestFixture]
public class CompleteTodoTests
{
    private ITodoRepository _repository;
    private CompleteTodoCommandHandler _handler;
    private Todo _todo;
    private CompleteTodoCommand _validCommand = new() { Id = 1 };

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new(_repository);
        _todo = Todo.Create("Testing Todos").Value;
    }

    [Test]
    public async Task CompleteTodoCommand_ValidId_ReturnsSuccess()
    {
        _repository
            .GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(Result.Ok(_todo));

        var result = await _handler.Handle(_validCommand, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _todo.Completed.Should().BeTrue();
        _repository.Received().Update(Arg.Is(_todo));
    }

    [Test]
    public async Task CompleteTodoCommand_InvalidId_ReturnsNotFoundError()
    {
        _repository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(Result.Fail(new NotFoundError(1, "Todo")));

        var result = await _handler.Handle(_validCommand, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainItemsAssignableTo<NotFoundError>();
    }
}
