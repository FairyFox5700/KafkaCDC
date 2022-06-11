using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaCDC.Common.Kafka
{
    public interface IKafkaHandler<in TKey, in TValue>
    {
        Task HandleAsync(TKey key, TValue value);
    }
}
