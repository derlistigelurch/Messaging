using System.Net;
using Confluent.Kafka;

Console.WriteLine("Money Laundering Service started");
var kafkaAddress = Environment.GetEnvironmentVariable("KAFKA_ADDRESS", EnvironmentVariableTarget.Process)!;
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = kafkaAddress,
    GroupId = "MoneyLaunderingService",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var producerConfig = new ProducerConfig
{
    BootstrapServers = kafkaAddress,
    ClientId = Dns.GetHostName(),
};

using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
consumer.Subscribe("Payments");

while (true)
{
    var consumeResult = consumer.Consume();
    if (int.Parse(consumeResult.Message.Value) <= 1000)
    {
        Console.WriteLine($"Received payment {consumeResult.Message.Value}");
        continue;
    }

    using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    producer.Produce("LaundryCheck", new Message<Null, string> {Value = consumeResult.Message.Value});
    producer.Flush();

    Console.WriteLine($"Send payment {consumeResult.Message.Value}. Need AML check.");
}