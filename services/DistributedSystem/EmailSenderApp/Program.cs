using EmailSenderApp.Services.IServices;
using EmailSenderApp.Services;
using CommonServices;
using EmailSenderApp.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers(options => {
    options.Conventions.Add(new RoutePrefixConvention("api/emailsender"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add logging configuration
builder.Logging.AddConsole(); // Add Console logging provider
builder.Logging.SetMinimumLevel(LogLevel.Information); // Set the minimum logging level
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddHostedService<MailReceiver>();
var app = builder.Build();

app.MapDefaultEndpoints();

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
