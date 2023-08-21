using Application.TodoLists.ArchiveTodoList;
using Domain.Common.Errors;
using Domain.Common.ValueObjects;
using Domain.TodoLists;
using FluentResults;

namespace Application.UnitTests.TodoLists;

[TestFixture]
public class ArchiveTodoListTests
{
    private ITodoListRepository _repository;
    private ArchiveTodoListCommandHandler _handler;
    private readonly ArchiveTodoListCommand _validCommand = new() { TodoListId = 1 };
    private TodoList _todoList;

    [SetUp]
    public void Setup()
    {
        _todoList = TodoList.Create(Title.Create("New Todo List").Value);
        _repository = Substitute.For<ITodoListRepository>();
        _repository.GetByIdAsync(Arg.Is<int>(1), Arg.Any<CancellationToken>()).Returns(Result.Ok(_todoList));
        _repository.GetByIdAsync(Arg.Is<int>(2), Arg.Any<CancellationToken>()).Returns(Result.Fail(new NotFoundError(2, "TodoList")));

        _handler = new(_repository);
    }

    [Test]
    public async Task ArchiveTodoListCommand_WithExistingTodoList_SetsArchivedTrue()
    {
        await _handler.Handle(_validCommand, CancellationToken.None);

        _todoList.Archived.Should().BeTrue();
        _repository
            .Received()
            .Update(Arg.Is<TodoList>(x => x.Archived == true));
    }

    [Test]
    public async Task ArchiveTodoListCommand_WithExistingTodoList_ReturnsSuccess()
    {
        var result = await _handler.Handle(_validCommand, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
    }

    [Test]
    public async Task ArchiveTodoListCommand_WithMissingTodoList_ReturnsFailure()
    {
        var command = new ArchiveTodoListCommand() { TodoListId = 2 };
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
    }

    [Test]
    public void ArchiveTodoListCommandValidator_WithValidCommand_ReturnsIsValidTrue()
    {
        var validator = new ArchiveTodoListCommandValidator();
        var result = validator.Validate(_validCommand);

        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void ArchiveTodoListCommandValidator_WithDefaultTodoListId_ReturnsIsValidFalse()
    {
        var validator = new ArchiveTodoListCommandValidator();

        var result = validator.Validate(new ArchiveTodoListCommand() { TodoListId = default });

        result.IsValid.Should().BeFalse();
    }
}
