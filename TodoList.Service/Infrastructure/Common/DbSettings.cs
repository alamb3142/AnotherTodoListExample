using Microsoft.Data.SqlClient;

namespace Infrastructure.Common;

public class DbSettings
{
	public string ConnectionString => GetConnectionString();
	public required string Server { get; set; }
	public required string DatabaseName { get; set; }

	private string GetConnectionString()
	{
		var connectionString = new SqlConnectionStringBuilder
		{
			DataSource = Server,
			InitialCatalog = DatabaseName,
			IntegratedSecurity = true,
			TrustServerCertificate = true
		};

		return connectionString.ToString();
	}


}
