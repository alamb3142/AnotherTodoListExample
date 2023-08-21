using Application.Todos.CreateTodo;
using Domain.Common.Errors;
using Domain.Common.ValueObjects;
using Domain.Todos;

namespace Application.UnitTests.Todos.CreateTodo;

[TestFixture]
public class CreateTodoTests
{
    private ITodoRepository _repository;
    private CreateTodoCommandHandler _handler;
    private Todo _todo;
    private CreateTodoCommand _validCommand = new() { Title = "A New Todo" };

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new(_repository);
        _todo = Todo.Create(Title.Create("A New Todo").Value);
    }

    [Test]
    public async Task CreateTodoCommand_WithValidTitle_CreatesTodo()
    {
        var result = await _handler.Handle(_validCommand, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _repository.Received().Create(Arg.Any<Todo>());
    }

    [TestCase("")]
    [TestCase("A really really really really really really really really really long title")]
    [TestCase("     ")]
    public async Task CreateTodoCommand_WithInvalidTitle_ReturnsBusinessRuleError(string title)
    {
        var command = new CreateTodoCommand() { Title = title };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainItemsAssignableTo<BusinessRuleError>();
    }
}
