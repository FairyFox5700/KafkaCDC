
using System.Reflection;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace KafkaCDC.Common.Kafka
{
    public static class KafkaRegistration
    {
        public static IServiceCollection AddKafkaConsumer<TKey, TValue, THandler>(
            this IServiceCollection services,
            Action<KafkaConsumerConfig> configAction) where THandler : class, IKafkaHandler<TKey, TValue>
        {
            services.AddScoped<IKafkaHandler<TKey, TValue>, THandler>();

            services.AddHostedService<BackGroundKafkaConsumer<TKey, TValue>>();

            services.Configure(configAction);

            return services;
        }
    }
}
