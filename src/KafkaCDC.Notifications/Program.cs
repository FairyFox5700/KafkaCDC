using KafkaCDC.Common.Kafka;
using KafkaCDC.Notifications.Configs;
using KafkaCDC.Notifications.Consumers;
using KafkaCDC.Notifications.Email;
using KafkaCDC.Notifications.Events;
using KafkaCDC.Notifications.Events.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentEmailServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<TraderCreatedKafkaConsumerConfigs>(
                            builder.Configuration.GetSection(nameof(TraderCreatedKafkaConsumerConfigs)))
                .Configure<TraderSubscriptionsKafkaConsumerConfigs>(
                            builder.Configuration.GetSection(nameof(TraderSubscriptionsKafkaConsumerConfigs)));
builder.Services.AddScoped<IKafkaHandler<string, TraderCreatedEvent>, TraderCreatedEventHandler>();
builder.Services.AddScoped<IKafkaHandler<string, DealSubscribedTradersUpdatedEvent>, DealSubscribedTradersUpdatedEventHandler>();
builder.Services.AddHostedService<TradersCreatedConsumer>();
builder.Services.AddHostedService<TradersSubscriptionConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
