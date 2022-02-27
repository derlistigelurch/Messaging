using System.Net;
using Confluent.Kafka;

Console.WriteLine("Start Core Banking Service");
var kafkaAddress = Environment.GetEnvironmentVariable("KAFKA_ADDRESS", EnvironmentVariableTarget.Process)!;
var random = new Random();
var config = new ProducerConfig
{
    BootstrapServers = kafkaAddress,
    ClientId = Dns.GetHostName(),
};

using var producer = new ProducerBuilder<Null, string>(config).Build();
while (true)
{
    var payment = random.Next(500, 1500).ToString();
    producer.Produce("Payments", new Message<Null, string> {Value = payment});
    producer.Flush();

    Console.WriteLine($"Send payment {payment}");
    Thread.Sleep(4000);
}