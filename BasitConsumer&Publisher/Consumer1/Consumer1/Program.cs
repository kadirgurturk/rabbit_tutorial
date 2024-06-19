// See https://aka.ms/new-console-template for more information

// Temel düzeyde publisher

// 1. RabbitMq sunucusuna bağlantı
// 2. bağlantıyı aktiflerştirme
// 3. Que oluşturma
// 4. Queue'ye mesajı okuma

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Bağlantı oluşturuldu
ConnectionFactory factory = new();
factory.Uri = new("amqps://aiohjrdn:UFqTC2u3AMj-4lzJrDWLJY0Znb-oDINo@goose.rmq2.cloudamqp.com/aiohjrdn");

// Bağlantıyı aktifleştir
using var conneciton = factory.CreateConnection();
using var channel = conneciton.CreateModel();

//Queue oluşturma ----> bu yapı publisher ile aynı yapı olmalı
channel.QueueDeclare(queue: "example", exclusive: false);

// Queue'dan mesaj okumak için event sınıflarnı kullanırız
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example", false, consumer);
consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
