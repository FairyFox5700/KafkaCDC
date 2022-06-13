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

builder.Services.AddRazorPages();


builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq(serverUrl: builder.Configuration.GetValue<string>("SeqUrl")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.Run();
