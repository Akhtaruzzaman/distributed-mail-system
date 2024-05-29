using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using CommonServices;
using Microsoft.Extensions.Options;

namespace AccountsApp.Services
{
    public class NotificationReceiver : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ILogger<NotificationReceiver> _logger { get; }

        public NotificationReceiver(IOptions<RabbitMQSettings> options, ILogger<NotificationReceiver> logger)
        {
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
            _channel.QueueDeclare(queue: "acc_callback_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Received notification: {message}");
            };

            _channel.BasicConsume(queue: "acc_callback_queue", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
