// See https://aka.ms/new-console-template for more information

// Temel düzeyde publisher

// 1. RabbitMq sunucusuna bağlantı
// 2. bağlantıyı aktiflerştirme
// 3. Que oluşturma
// 4. Queue'ye mesajı gönderme

using System.Text;
using RabbitMQ.Client;

// Bağlantı oluşturuldu
ConnectionFactory factory = new();
factory.Uri = new("amqps://eqqzgmee:0ZpLq3Z1_mgDvAmTstZkZXxO7fS_Ustx@goose.rmq2.cloudamqp.com/eqqzgmee");

// Bağlantıyı aktifleştir
using var conneciton = factory.CreateConnection();
using var channel = conneciton.CreateModel();

//Queue oluşturma ----> bu yapı publisher ile aynı yapı olmalı, ilk parametre adı, exclusive ise bu kuyruğun özel olup olamdığını yani birden fazla bağlantı tarafından kullanıp kullanılamaycağını işaret eder.
channel.QueueDeclare(queue: "example", exclusive: false);

//Queue'ya mesaj gönderme, RabbitMq mesajı byte türünden kabul eder, mesajları byte'a çevirmeiliyiz
var messeage = Encoding.UTF8.GetBytes("Merhaba Kadir");

//Sıra göndermede mesajı, defeault excahnge-->directExchange, directExchange'de routingKey, queue ismi olmalı,
channel.BasicPublish(exchange:"", routingKey: "example", body: messeage);

Console.Read();