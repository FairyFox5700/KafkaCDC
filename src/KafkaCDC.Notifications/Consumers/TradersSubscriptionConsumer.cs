using KafkaCDC.Common.Kafka;
using KafkaCDC.Notifications.Configs;
using KafkaCDC.Notifications.Events;
using Microsoft.Extensions.Options;

namespace KafkaCDC.Notifications.Consumers
{
    public class TradersSubscriptionConsumer : BackGroundKafkaConsumer<string, DealSubscribedTradersUpdatedEvent>
    {
        public TradersSubscriptionConsumer(
            IOptions<TraderSubscriptionsKafkaConsumerConfigs> config,
            IServiceScopeFactory serviceScopeFactory) 
            : base(config, serviceScopeFactory)
        {
        }
    }
}
