using CommonServices;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using EmailSenderApp.Services.IServices;
using Microsoft.Extensions.Options;

namespace EmailSenderApp.Services
{
    public class MailReceiver : BackgroundService
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<MailReceiver> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MailReceiver(IEmailSenderService emailSenderService, IOptions<RabbitMQSettings> options, ILogger<MailReceiver> logger)
        {
            _emailSenderService = emailSenderService;
            this._logger = logger;
            var rabbitMQSettings = options.Value;
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQSettings.HostName,
                Port = rabbitMQSettings.Port,
                UserName = rabbitMQSettings.UserName,
                Password = rabbitMQSettings.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "email_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Mail Receiver: {message}");
                var emailMessage = JsonSerializer.Deserialize<EmailMessage>(message);
                await _emailSenderService.SendEmailAsync(emailMessage);
                NotifyOriginalService(emailMessage.CallbackQueue, emailMessage); // Notify original service
            };

            _channel.BasicConsume(queue: "email_queue", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        private void NotifyOriginalService(string callbackQueue, EmailMessage emailMessage)
        {
            var notification = new { Status = "Email Sent", EmailMessage = emailMessage };
            var notificationMessage = JsonSerializer.Serialize(notification);
            var body = Encoding.UTF8.GetBytes(notificationMessage);
            _channel.BasicPublish(exchange: "", routingKey: callbackQueue, basicProperties: null, body: body);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
