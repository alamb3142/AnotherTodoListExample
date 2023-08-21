using Application.Todos.RenameTodo;
using Domain.Common.Errors;
using Domain.Common.ValueObjects;
using Domain.Todos;
using FluentResults;

namespace Application.UnitTests.Todos.RenameTodo;

[TestFixture]
public class RenameTodoTests
{
    private ITodoRepository _repository;
    private RenameTodoCommandHandler _handler;
    private Todo _todo;
    private RenameTodoCommand _validCommand = new() { Id = 1, Title = "A New Todo" };

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new(_repository);
        _todo = Todo.Create(Title.Create("An Old Todo").Value);
    }

    [Test]
    public async Task RenameTodoCommand_WithValidTitle_RenamesTodo()
    {
        _repository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(_todo);

        var result = await _handler.Handle(_validCommand, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repository.Received().Update(Arg.Is(_todo));
    }

    [Test]
    public async Task RenameTodoCommand_NonExistentTodo_ReturnsNotFoundError()
    {
        _repository
            .GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(Result.Fail(new NotFoundError(1, "Todo")));

        var result = await _handler.Handle(_validCommand, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainItemsAssignableTo<NotFoundError>();
        _repository.DidNotReceive().Update(Arg.Any<Todo>());
    }

    [TestCase("")]
    [TestCase("      ")]
    [TestCase("A really really really really really really reeeaaaaly long title")]
    public async Task RenameTodoCommand_InvalidTitle_ReturnsBusinessRuleError(string title)
    {
        _repository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(Result.Ok(_todo));
        var command = new RenameTodoCommand() { Id = 1, Title = title };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainItemsAssignableTo<BusinessRuleError>();
        _repository.DidNotReceive().Update(Arg.Any<Todo>());
    }
}
