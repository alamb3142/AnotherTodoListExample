using Application;
using Infrastructure;
using Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.Configure<DbSettings>(config);
var dbSettings = builder.Configuration.GetSection("DbSettings").Get<DbSettings>()!;

builder.Services.AddApplication();
builder.Services.AddInfrastructure(dbSettings);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
