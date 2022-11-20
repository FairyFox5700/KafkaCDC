
using Confluent.Kafka;

namespace KafkaCDC.Common.Kafka
{
    public class KafkaConsumerConfig : ConsumerConfig
    {
        public string? Topic { get; set; }
        public KafkaConsumerConfig()
        {
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            EnableAutoOffsetStore = false;
        }
    }

}
