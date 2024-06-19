using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairRequest.Application.Abstraction
{
    public class MqService : IMqService
    {
        private readonly IConfiguration _configuration;
        public MqService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMessage(string queue, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetConnectionString("MqConnection"),
                
                UserName = "guest",
                Password = "guest",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                routingKey: queue,
                basicProperties: null,
                body: body);

        }
    }
}
