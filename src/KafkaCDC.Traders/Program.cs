using System.Reflection;
using System.Text.Json.Serialization;

using KafkaCDC.Common.Kafka;
using KafkaCDC.Traders.Commands;
using KafkaCDC.Traders.Domain;
using KafkaCDC.Traders.Events;
using KafkaCDC.Traders.Events.Handlers;
using KafkaCDC.Traders.KafkaCDC.Deals;

using MediatR;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TradersDbContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<SetUpHostedService>();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: builder.Configuration.GetValue<string>("SeqUrl")));
builder.Services.AddKafkaConsumer<string, DealUpdatedEvent, DealUpdatedEventHandlers>(p =>
{
    p.Topic = "outbox.event.deals";
    p.GroupId = "deals_events_traders_group";
    p.BootstrapServers = "localhost:9092";
});
builder.Services.AddMediatR(typeof(TraderSubscriptionAddedCommand).GetTypeInfo().Assembly);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();