using Application.Todos;
using Infrastructure.Common;
using Application.Todos.Common;
using Dapper;

namespace Infrastructure.Todos;

public class TodoReadRepository : ITodoReadRepository
{
	private readonly ISqlConnectionFactory connectionFactory;

	public TodoReadRepository(ISqlConnectionFactory connectionFactory)
	{
		this.connectionFactory = connectionFactory;
	}

	public async Task<List<TodoDto>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var query =
			@"
			SELECT Id, Title = Title_Value, Completed
			FROM Todos
		";

		using var connection = await connectionFactory.GetOpenConnectionAsync(cancellationToken);
		var result = await connection.QueryAsync<TodoDto>(query);

		return result.ToList();
	}

	public async Task<List<TodoDto>> GetFilteredAsync(
		string? searchTerm,
		int offset,
		int fetchNum,
		CancellationToken cancellationToken = default
	)
	{
		var filter = string.IsNullOrWhiteSpace(searchTerm)
			? " WHERE Title LIKE '%' + @SearchTerm + '%'"
			: "";

		var sql =
			@"
			SELECT
				Id,
				Title = Title_Value,
				Completed
			FROM Todos"
			+ filter
			+ " OFFSET @Offset ROWS"
			+ " FETCH FIRST @FetchNum;";

		var parameters = new DynamicParameters();
		parameters.Add("SearchTerm", searchTerm);
		parameters.Add("FetchNum", fetchNum);
		parameters.Add("Offset", offset);

		using var connection = await connectionFactory.GetOpenConnectionAsync(cancellationToken);
		var result = await connection.QueryAsync<TodoDto>(sql, parameters);

		return result.ToList();
	}

	public async Task<IEnumerable<TodoDto>> GetForListAsync(int todoListId, CancellationToken cancellationToken = default)
	{
		var sql = @"
			SELECT Id,
				   Title = Title_Value,
				   Completed
			FROM Todos
			WHERE TodoListId = @TodoListId";
		var parameters = new DynamicParameters();
		parameters.Add("TodoListId", todoListId);

		using var connection = await connectionFactory.GetOpenConnectionAsync(cancellationToken);
		var result = await connection.QueryAsync<TodoDto>(sql, parameters);

		return result;
	}
}
