namespace Application.Todos.Common;

public class TodoDto
{
	public required int Id { get; init; }
	public required string Title { get; init; }
	public required bool Completed { get; init; }
	public int? TodoListId { get; init; }
}
