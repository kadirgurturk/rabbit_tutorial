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
factory.Uri = new("amqps://eqqzgmee:0ZpLq3Z1_mgDvAmTstZkZXxO7fS_Ustx@goose.rmq2.cloudamqp.com/eqqzgmee");

//Bağlantıyı aktifleştirmw
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
 
//Queue oluşturma, publisher'daki gibi tanmlamalıyız birebir aynı olamalı
channel.QueueDeclare(queue: "example", exclusive: false);

//Queue'den Mesaj okuma, event channle'de gerçekleşmesi için dinlemeye alındı
EventingBasicConsumer consumer = new(channel);

channel.BasicConsume("example",false, consumer);

consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yer
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();