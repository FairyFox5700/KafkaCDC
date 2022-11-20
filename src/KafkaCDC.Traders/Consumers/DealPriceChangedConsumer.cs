using KafkaCDC.Common.Kafka;
using KafkaCDC.Traders.Configs;
using KafkaCDC.Traders.Events;
using KafkaCDC.Traders.Events.Handlers;

using Microsoft.Extensions.Options;

namespace KafkaCDC.Traders.Consumers
{
    public class DealPriceChangedConsumer : BackGroundKafkaConsumer<string, DealPriceChangedEvent>
    {
        public DealPriceChangedConsumer(
            IOptions<DealPriceChangedKafkaConsumerConfigs> config,
            IServiceScopeFactory serviceScopeFactory) 
            : base(config, serviceScopeFactory)
        {
        }
    }
}
