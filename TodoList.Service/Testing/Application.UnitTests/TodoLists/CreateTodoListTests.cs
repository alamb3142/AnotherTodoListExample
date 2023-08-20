using Application.TodoLists.CreateTodoList;
using Domain.Common.Errors;
using Domain.TodoLists;

namespace Application.UnitTests.TodoLists.CreateTodoList;

[TestFixture]
class CreateTodoListTests
{
	private CreateTodoListCommandHandler _handler;

	private ITodoListRepository _repo;
	private readonly CreateTodoListCommand _validCommand = new() { Name = "Todo List" };

	[SetUp]
	public void Setup()
	{
		_repo = Substitute.For<ITodoListRepository>();
		_handler = new(_repo);
	}

	[Test]
	public async Task CreateTodoListCommand_WithValidTitle_ReturnsSuccess()
	{
		var result = await _handler.Handle(_validCommand, CancellationToken.None);

		_repo.Received().Create(Arg.Any<TodoList>());
		result.IsSuccess.Should().BeTrue();
	}

	[Test]
	public async Task CreateTodoListCommand_WithInvalidTitle_ReturnsFailure()
	{
		var command = new CreateTodoListCommand() { Name = "" };

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsFailed.Should().BeTrue();
		result.Errors.Should().ContainItemsAssignableTo<BusinessRuleError>();
	}

	[Test]
	public async Task CreateTodoListCommand_WithInvalidTitle_DoesNotCallRepository()
	{
		var command = new CreateTodoListCommand() { Name = "" };

		var result = await _handler.Handle(command, CancellationToken.None);

		_repo.DidNotReceive().Create(Arg.Any<TodoList>());
	}

	[Test]
	public async Task CreateTodoListCommand_WhenRepositoryThrows_ThrowsException()
	{
		_repo.When(x => x.Create(Arg.Any<TodoList>())).Do(x => { throw new Exception(); });

		var act = async () => await _handler.Handle(_validCommand, CancellationToken.None);

		await act.Should().ThrowAsync<Exception>();
	}
}
