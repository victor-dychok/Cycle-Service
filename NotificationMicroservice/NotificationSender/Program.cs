using System.Reflection;
using System.Text;
using System.Text.Json;
using NotificationApplication;
using NotificationSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory {
    HostName = "localhost",

    UserName = "guest",
    Password = "guest",
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

// declare a server-named queue
channel.QueueDeclare(queue: "request-status",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

Console.WriteLine(" [*] Waiting for logs.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var serialaizedMessage = JsonSerializer.Deserialize<EmailMessageMq>(message);

    IEmailSender emailSender = new EmailSenderService();
    emailSender.SendEmailAsync(serialaizedMessage.Email, serialaizedMessage.Header, serialaizedMessage.Message + "\nThis message was sent automaticly. Do not replay to it.");

    Console.WriteLine($" # Adress {serialaizedMessage.Email} - Header {serialaizedMessage.Header} - Message {serialaizedMessage.Message}");
};
channel.BasicConsume(queue: "request-status",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();