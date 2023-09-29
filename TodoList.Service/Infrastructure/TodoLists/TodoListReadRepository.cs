using Application.TodoLists.Common;
using Dapper;
using Infrastructure.Common;

namespace Infrastructure.TodoLists;

internal class TodoListReadRepository : ITodoListReadRepository
{
	private readonly ISqlConnectionFactory connectionFactory;

	public TodoListReadRepository(ISqlConnectionFactory connectionFactory)
	{
		this.connectionFactory = connectionFactory;
	}

	public async Task<IEnumerable<TodoListSummaryDto>> GetTodoListSummariesAsync(CancellationToken cancellationToken)
	{
		var query = @"
			SELECT 
				[Id], 
				Title = [Title_Value],
				TodoCount = (
					SELECT COUNT(*)
					FROM [dbo].[Todos] AS t
					WHERE t.TodoListId = tl.Id
						AND t.Completed = 0
				)
			FROM [dbo].[TodoLists] AS tl";

		using var connection = await connectionFactory.GetOpenConnectionAsync(cancellationToken);
		var result = await connection.QueryAsync<TodoListSummaryDto>(query);

		return result.ToList();
		throw new NotImplementedException();
	}
}
