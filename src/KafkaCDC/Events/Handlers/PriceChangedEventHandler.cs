using KafkaCDC.Common.Kafka;
using KafkaCDC.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaCDC.Events.Handlers
{
    internal class PriceChangedEventHandler : IKafkaHandler<string, PriceChangedEvent>
    {
        public Task HandleAsync(string key, PriceChangedEvent value)
        {
            throw new NotImplementedException();
        }
    }
}
