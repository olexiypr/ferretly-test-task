using Ferretly.TestTask.TimeTrackingApi.DbContext;
using Ferretly.TestTask.TimeTrackingApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<TimeTrackerDbContext>(opt =>
{
    var host = builder.Configuration["Database:Host"];
    var port = builder.Configuration["Database:Port"];
    var password = builder.Configuration["Database:Password"];
    var username = builder.Configuration["Database:Username"];
    var databaseName = builder.Configuration["Database:DatabaseName"];
    opt.UseSqlServer($"Server={host};Database={databaseName};User Id={username};Password={password};TrustServerCertificate=true;");
});

builder.Services.AddCustomServices();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "Ferretly.TestTask.TimeTrackingApi v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();