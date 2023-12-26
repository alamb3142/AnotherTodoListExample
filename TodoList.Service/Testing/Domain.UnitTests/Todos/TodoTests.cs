using Domain.Common.ValueObjects;
using Domain.Todos;

namespace Testing.Domain.Todos;

[TestFixture]
public class TodoTests
{
	private readonly Title validTitle = Title.Create("Do the dishes!").Value;

	[Test]
	public void Create_ValidParams_ReturnsTodo()
	{
		var title = Title.Create("Wash the dishes!").Value;
		var result = Todo.Create(title);
		result.Title.Value.Should().Be("Wash the dishes!");
	}

	[Test]
	public void Complete_CompletesTodo()
	{
		var todo = Todo.Create(validTitle);
		todo.Complete();

		todo.Completed.Should().Be(true);
	}

	[Test]
	public void UpdateTitle_UpdatesTitle()
	{
		var todo = Todo.Create(validTitle);

		var newTitle = "Walk the dog";
		todo.Rename(newTitle);

		todo.Title.Value.Should().Be(newTitle);
	}
}
