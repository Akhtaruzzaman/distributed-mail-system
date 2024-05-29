using AccountsApp.Services;
using AccountsApp.Services.IServices;
using CommonServices;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add logging configuration
builder.Logging.AddConsole(); // Add Console logging provider
builder.Logging.SetMinimumLevel(LogLevel.Information); // Set the minimum logging level
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IEmailRequestService, EmailRequestService>();
builder.Services.AddHostedService<NotificationReceiver>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();