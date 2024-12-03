//Message Durability

// Consumer kaynaklı bir durumdan solayı mesajların nasıl davranması gerektiğini işledik.

// Ama direkt sunucu kaynaklı bir kapanma veya sorun oluştuğunda ne olcapını konuşmadık.

// Sunucu kapanırsa default olarak tüm mesajlar silinecektir. Bu durumda Publisher'da ekstra konfigürsayon yapmamız gerekmektedir.

// Aşağıda 3 adımla bu konfigürsayon ile mesajlar kalıcı hale gelebilirç

using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new("amqps://eqqzgmee:0ZpLq3Z1_mgDvAmTstZkZXxO7fS_Ustx@goose.rmq2.cloudamqp.com/eqqzgmee");

using var connection = factory.CreateConnection(); // aktifleşme tamamlandı
using var model = connection.CreateModel(); // kanak oluşturma

model.QueueDeclare(queue: "example", exclusive: false, durable:true);
// 1) Kuyruğu yarattığımız QueueDeclare methodunda durable parametresini true vermmeiz gerekir.


// 3) IBasicProperties nesnesini yaratalım.ve persistence field'ına true verleim
IBasicProperties basicProperties = model.CreateBasicProperties();
basicProperties.Persistent = true;

var message = Encoding.UTF8.GetBytes("FirstRabbit");
model.BasicPublish(exchange:"",routingKey:"example",body:message, basicProperties:basicProperties);
// 2)Publish edilen mesajları korumak için, .BasicPublish methodunun "basicProperties" methoduna IBasicProperties sınıfından bir nesne vermemiz gerekir.


Console.Read();