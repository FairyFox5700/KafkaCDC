using KafkaCDC.Common.Kafka;
using KafkaCDC.Notifications.Configs;
using KafkaCDC.Notifications.Events;
using KafkaCDC.Notifications.Events.Handlers;

using Microsoft.Extensions.Options;

namespace KafkaCDC.Notifications.Consumers
{
    public class TradersCreatedConsumer : BackGroundKafkaConsumer<string, TraderCreatedEvent>
    {
        public TradersCreatedConsumer(IOptions<TraderCreatedKafkaConsumerConfigs> config,
            IServiceScopeFactory serviceScopeFactory) 
            : base(config, serviceScopeFactory)
        {
        }
    }
}
