//Bu bölümde gönderilen mesajların silinme davranışı üzerine olan özelleştirmelere bakıcaz.

//Rabbitmq'da Kuyruğa gönderlien mesajlar default olarak direkt silinnir olarak işaretlenir. Aksi belirtilmediği sürece consumer'da hata yaşanssa bile RabbitMQ mesajı silinebilir olarak işaretler.

//Mesajların doğru şekilde gidip gitmediğini anlamak için consumer tarafınsda bu mesajları iletilde olarak işaretleyip öyle silinme işlemini yaptırmalıyız.

// Bu iş için geliştirilen Yapını adı******************  MESSAGE ACKNOWLEDMENT'tır *************************

// Bu yapı aktifken gönderilen bir mesaj silindi olarak işaretlenmez ise yarım saat sonra tekrar bir consumer'a gönderilir. bundan dolayı bir mesaj başarılı şekilde işlenirse'de bu mesajı silindi olarak işaretlenmesi lazım

//Consumer tarafında mesajların iletildiğini işrateleyen fonksiyonın Adı ****************BasicAck'tır.******************

//Bu fonskiyonu kullanabilmel için channel nesnesinin "autoHack" değeri false olmalı

// Daha sonra channel.BasicAck(e.DeliveryTag, false) fonsiyonu içinde gerekli configurationlar yapılmalı

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
    channel.BasicAck(e.DeliveryTag, false); // İlk olarak unique bir id olan deliveryTag değerini veriyoruz.
    // ikinci parametre ise sadece bu mesaj mı iletildi olarak iletilsin, yoksa bundan öncekilerde otomatik iletildi densin mi karar veririz.
};

Console.Read();
