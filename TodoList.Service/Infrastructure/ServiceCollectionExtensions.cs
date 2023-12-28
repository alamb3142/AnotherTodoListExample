using Application.TodoLists.Common;
using Application.Todos;
using Domain.TodoLists;
using Domain.Todos;
using Domain.Users;
using Infrastructure.Common;
using Infrastructure.TodoLists;
using Infrastructure.Todos;
using Infrastructure.Users;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, DbSettings settings)
	{
		services.AddScoped<ITodoRepository, TodoRepository>();
		services.AddScoped<ITodoReadRepository, TodoReadRepository>();
		services.AddScoped<ITodoListRepository, TodoListRepository>();
		services.AddScoped<ITodoListReadRepository, TodoListReadRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		services.AddSingleton<ISqlConnectionFactory>(
			new SqlConnectionFactory(settings.ConnectionString)
		);

		services.AddDbContext<TodoListContext>(options =>
		{
			options.UseSqlServer(settings.ConnectionString);
		});

		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
	}
}
