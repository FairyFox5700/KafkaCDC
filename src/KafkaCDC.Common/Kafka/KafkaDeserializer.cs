
using Confluent.Kafka;

using Newtonsoft.Json;

using System.Text;

namespace KafkaCDC.Common.Kafka
{
    public class KafkaDeserializer<T> : IDeserializer<T?>
    {
        public T? Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var dataJsonString = Encoding.UTF8.GetString(data);

            var jsonString = JsonConvert.DeserializeObject<string>(dataJsonString);
            if (jsonString == null)
            {
                throw new InvalidOperationException(nameof(jsonString));
            }

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
