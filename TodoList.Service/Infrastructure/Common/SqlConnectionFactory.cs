using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Common;

public interface ISqlConnectionFactory
{
	public Task<IDbConnection> GetOpenConnectionAsync(CancellationToken cancellationToken = default);
}

public class SqlConnectionFactory : ISqlConnectionFactory
{
	private readonly string _connectionString;

	public SqlConnectionFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<IDbConnection> GetOpenConnectionAsync(CancellationToken cancellationToken = default)
	{
		var connection = new SqlConnection(_connectionString);
		await connection.OpenAsync(cancellationToken);
		return connection;
	}
}
