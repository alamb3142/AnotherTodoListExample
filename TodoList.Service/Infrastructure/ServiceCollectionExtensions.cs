using Application.Todos;
using Domain.Todos;
using Infrastructure.Common;
using Infrastructure.Todos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, DbSettings settings)
	{
		services.AddScoped<ITodoRepository, TodoRepository>();
		services.AddScoped<ITodoReadRepository, TodoReadRepository>();

		var connectionString = new SqlConnectionStringBuilder();
		connectionString.DataSource = "localhost";
		connectionString.InitialCatalog = "TodoList";

		services.AddSingleton<ISqlConnectionFactory>(
			new SqlConnectionFactory(settings.ConnectionString)
		);

		services.AddDbContext<TodoListContext>(options =>
		{
			options.UseSqlServer(settings.ConnectionString);
		});
	}
}
