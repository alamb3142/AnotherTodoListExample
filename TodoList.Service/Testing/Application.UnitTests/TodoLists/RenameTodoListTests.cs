using Application.TodoLists.Rename;
using Domain.Common.Errors;
using Domain.Common.ValueObjects;
using Domain.TodoLists;
using FluentResults;

namespace Application.UnitTests.RenameTodoList;

[TestFixture]
public class RenameTodoListTests
{
	private ITodoListRepository _repository;
	private RenameTodoListCommandHandler _handler;
	private RenameTodoListCommand _validCommand = new() { Id = 1, Name = "A TodoList" };
	private TodoList _todoList;

	[SetUp]
	public void Setup()
	{
		_repository = Substitute.For<ITodoListRepository>();
		_handler = new(_repository);
		_todoList = TodoList.Create(Title.Create("Another TodoList").Value);
	}

	[Test]
	public async Task RenameTodoList_WithValidName_RenamesTodo()
	{
		var originalName = _todoList.Title.Value;
		_repository
			.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
			.Returns(Result.Ok(_todoList));

		var result = await _handler.Handle(_validCommand, CancellationToken.None);
		var newName = _todoList.Title.Value;

		result.IsSuccess.Should().BeTrue();
		newName.Should().NotBeEquivalentTo(originalName);
		newName.Should().BeEquivalentTo(_validCommand.Name);
		_repository.Received().Update(Arg.Is(_todoList));
	}

	[Test]
	public async Task RenameTodoList_WithInvalidId_ReturnsNotFoundError()
	{
		_repository
			.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
			.Returns(Result.Fail(new NotFoundError(1, nameof(TodoList))));

		var result = await _handler.Handle(_validCommand, CancellationToken.None);

		result.IsFailed.Should().BeTrue();
		result.Errors.Should().ContainItemsAssignableTo<NotFoundError>();
	}

	[Test]
	public async Task RenameTodoList_WithBadTitle_ReturnsBusinessRuleError()
	{
		_repository
			.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
			.Returns(Result.Ok(_todoList));
		var command = new RenameTodoListCommand() { Id = 1, Name = "" };

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsFailed.Should().BeTrue();
		result.Errors.Should().ContainItemsAssignableTo<BusinessRuleError>();
	}
}
