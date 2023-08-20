using Application.Todos.Common;

namespace Application.Todos.GetAllTodos;

public class GetAllTodosQueryResponse
{
    public required IEnumerable<TodoDto> Todos { get; init; }
}
