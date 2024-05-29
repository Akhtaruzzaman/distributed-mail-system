using CommonServices;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using AccountsApp.Services.IServices;
using Microsoft.Extensions.Options;

namespace AccountsApp.Services
{
    public class EmailRequestService: IEmailRequestService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public EmailRequestService(IOptions<RabbitMQSettings> options)
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
        }

        public void SendEmailRequest(EmailMessage emailMessage)
        {
            var message = JsonSerializer.Serialize(emailMessage);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "email_queue", basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }

}
