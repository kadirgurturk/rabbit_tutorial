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
using var connection = factory.CreateConnection(); // aktifleşme tamamlandı
using var model = connection.CreateModel(); // kanak oluşturma

//Queue oluşturma --> queue:"example": kuyruğun adı ve RoutingKey'i ---- ,exclusive:false: bu kuyruğun tek bir kanaldan dinlenip dinlenilemeyeciğine karan veren argümandır.
model.QueueDeclare(queue: "example", exclusive: false);

// Queue'ye mesaj gönderme, RabbitMq kuyruğu atacağı mesajları byte türünden kabul edilir.
// Bu sebeple bizim mesajları byte türünden alır ve gönderir.

var message = Encoding.UTF8.GetBytes("FirstRabbit");
model.BasicPublish(exchange:"",routingKey:"example",body:message);

Console.Read();