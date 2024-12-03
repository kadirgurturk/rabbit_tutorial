//Bu bölümde gönderilen mesajların silinme davranışı üzerine olan özelleştirmelere bakıcaz.

// Daha önceki bölümde BasicAck ile okunan mesajları silinebilir olarak işaretlemeyi işlemiştik, BasicAck ile başarlı şekilde işlenen mesajları silinebilir olarak işaretleyip geri göndermişik.

// Peki başarlı şekile işleyemezsek ve tekrar silme bunu demek için ne yapmamız lazım

// Bu durumda kullanmamız gereken özellik ****************** BASICNACK *****************************

// bu fonksiyon ile ilgili mesaj veya mesajları tekrar işletebiliriz



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
