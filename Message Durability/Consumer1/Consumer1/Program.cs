//Message Durability

// Consumer kaynaklı bir durumdan solayı mesajların nasıl davranması gerektiğini işledik.

// Ama direkt sunucu kaynaklı bir kapanma veya sorun oluştuğunda ne olcapını konuşmadık.

// Sunucu kapanırsa default olarak tüm mesajlar silinecektir.



using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new();
factory.Uri = new("amqps://eqqzgmee:0ZpLq3Z1_mgDvAmTstZkZXxO7fS_Ustx@goose.rmq2.cloudamqp.com/eqqzgmee");

using var conneciton = factory.CreateConnection();
using var channel = conneciton.CreateModel();


channel.QueueDeclare(queue: "example", exclusive: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example", false, consumer);
consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicNack(e.DeliveryTag, false, true);
    // Yine ilk parametre unique Id DeliveryTag
    // İkinci parametre ise kendisinden önce gelenlerde aynı şekil işaretlensin mi işaretlenmesin mi, false ise sadece ilgili mesaj silinmemeli diye işaretlenir.
    // Requeue ise yeni parametremiz. Bu prametre bu mesajın tekrar işlenmesi için kuyruğua tekrar alınmasına karar verir, true ise kayıt tekrar kuyruğ alınır ve işlenir.
};

Console.Read();
