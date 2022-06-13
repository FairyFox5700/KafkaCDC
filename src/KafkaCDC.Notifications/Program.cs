using KafkaCDC.Common.Kafka;
using KafkaCDC.Notifications.Email;
using KafkaCDC.Notifications.Events;
using KafkaCDC.Notifications.Events.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddFluentEmailServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddKafkaConsumer<string, TraderCreatedEvent, TraderCreatedEventHandler>(p =>
{
    p.Topic = "traders.events";
    p.GroupId = "trader_events_notification_group";
    p.BootstrapServers = "kafka:9092";
    p.AllowAutoCreateTopics = true;
});*/
builder.Services.AddKafkaConsumer<string, DealSubscribedTradersUpdatedEvent, DealSubscribedTradersUpdatedEventHandler>(p =>
{
    p.Topic = "subscriptions.events";
    p.GroupId = "deal_subscription_events_notification_group";
    p.BootstrapServers = "kafka:9092";
    p.AllowAutoCreateTopics = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
