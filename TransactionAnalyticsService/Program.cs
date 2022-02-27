using Confluent.Kafka;

Console.WriteLine("Transaction Analytics Service started");

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "TransactionAnalyticsService",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("LaundryCheck");

while (true)
{
    var consumeResult = consumer.Consume();
    Console.WriteLine($"Received payment {consumeResult.Message.Value}");
}