using System.Reflection;
using System.Text.Json.Serialization;

using KafkaCDC.Common.Kafka;
using KafkaCDC.Traders;
using KafkaCDC.Traders.Commands;
using KafkaCDC.Traders.Configs;
using KafkaCDC.Traders.Consumers;
using KafkaCDC.Traders.Domain;
using KafkaCDC.Traders.Events;
using KafkaCDC.Traders.Events.Handlers;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TradersDbContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<SetUpHostedService>();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: builder.Configuration.GetValue<string>("SeqUrl")));
builder.Services.Configure<DealPriceChangedKafkaConsumerConfigs>(
                            builder.Configuration.GetSection(nameof(DealPriceChangedKafkaConsumerConfigs)));
builder.Services.AddScoped<IKafkaHandler<string, DealPriceChangedEvent>, DealPriceChangedEventHandlers>(); 
builder.Services.AddHostedService<DealPriceChangedConsumer>();

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