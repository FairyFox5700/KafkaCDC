using System.Reflection;
using System.Text.Json.Serialization;

using KafkaCDC.Common.Kafka;
using KafkaCDC.Email;
using KafkaCDC.Events;
using KafkaCDC.Events.Handlers;
using KafkaCDC.Notificator.Events;
using KafkaCDC.Notificator.Events.Handlers;

using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddFluentEmailServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: builder.Configuration.GetValue<string>("SeqUrl")));
builder.Services.AddKafkaConsumer<string, TraderCreatedEvent, TraderCreatedEventHandler>(p =>
{
    p.Topic = "outbox.event.traders";
    p.GroupId = "trader_events_notification_group";
    p.BootstrapServers = "localhost:9092";
});
builder.Services.AddKafkaConsumer<string, DealSubscribedTradersUpdatedEvent, DealSubscribedTradersUpdatedEventHandler>(p =>
{
    p.Topic = "outbox.event.subscriptions";
    p.GroupId = "deal_subscription_events_notification_group";
    p.BootstrapServers = "localhost:9092";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
