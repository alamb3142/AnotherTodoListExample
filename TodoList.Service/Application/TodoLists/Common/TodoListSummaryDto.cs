namespace Application.TodoLists.Common;

public class TodoListSummaryDto
{
	public required int Id { get; init; }
	public required string Title { get; init; }
	public required int TodoCount { get; init; }
}
